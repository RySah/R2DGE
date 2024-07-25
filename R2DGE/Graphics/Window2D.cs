using R2DGE.Entity;
using R2DGE.NumericExt;
using R2DGE.Texture;
using SDL2;
using SkiaSharp;

namespace R2DGE.Graphics
{
    public record WindowStatus(bool Visible, float DeltaTime);

    public class Window2D: IDisposable
    {
        public const int WINDOWPOSCENTERED = SDL.SDL_WINDOWPOS_CENTERED;

        private nint _window;
        private nint _renderer;
        private bool _isWindowVisible;

        private ulong _previousTime;
        private ulong _currentTime;
        private float _deltaTime;

        public bool Visible => _isWindowVisible;

        public Window2D(string title, IntRect rect)
        {
            int result = SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            if (result != 0)
                throw new InvalidOperationException($"Unable to initialize SDL (result={result})");

            // center = SDL.SDL_WINDOWPOS_CENTERED
            _window = SDL.SDL_CreateWindow(title, rect.X, rect.Y, rect.Width, rect.Height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            _previousTime = SDL.SDL_GetPerformanceCounter();
            _isWindowVisible = true;
        }

        public Sprite CreateSprite(string path)
        {
            Texture2D texture = LoadTexture(path);
            return new Sprite(_window, _renderer, texture);
        }

        public void Maximize() => SDL.SDL_MaximizeWindow(_window);
        public bool IsMaximized()
        {
            int win_w, win_h;
            SDL.SDL_GetWindowSize(_window, out win_w, out win_h);
            int max_w, max_h;
            SDL.SDL_GetWindowMaximumSize(_window, out max_w, out max_h);
            return win_w == max_w && win_h == max_h;
        }

        public void GetWindowSize(out int w, out int h) 
            => SDL.SDL_GetWindowSize(_window, out w, out h);
        public void GetWindowMaximumSize(out int w, out int h) 
            => SDL.SDL_GetWindowMaximumSize(_window, out w, out h);

        public void Loop(Action<WindowStatus> loop)
        {
            bool running = true;
            SDL.SDL_Event e;
            ulong frequency;

            while (running)
            {
                _currentTime = SDL.SDL_GetPerformanceCounter();
                frequency = SDL.SDL_GetPerformanceFrequency();
                _deltaTime = (float)((_currentTime - _previousTime) / (double)frequency);
                _previousTime = _currentTime;

                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                        running = false;
                    else if (e.type == SDL.SDL_EventType.SDL_WINDOWEVENT)
                        HandleWindowEvent(e.window);
                }
                
                loop.Invoke(new WindowStatus(_isWindowVisible, _deltaTime));
            }

        } 

        public Renderer2D GetRenderer() => new(_renderer);

        private void HandleWindowEvent(SDL.SDL_WindowEvent windowEvent)
        {
            switch (windowEvent.windowEvent)
            {
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                    _isWindowVisible = true;
                    break;
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                    _isWindowVisible = false;
                    break;
            }
        }

        private Texture2D LoadTexture(string path)
        {
            // Load the image using SkiaSharp
            using (var codec = SKCodec.Create(path))
            {
                var info = new SKImageInfo(codec.Info.Width, codec.Info.Height);
                using (var bitmap = SKBitmap.Decode(codec))
                {
                    // Convert the image to a byte array
                    nint pixels = bitmap.GetPixels();

                    // Create an SDL surface
                    var surface = SDL.SDL_CreateRGBSurfaceWithFormatFrom(
                        pixels,
                        bitmap.Width,
                        bitmap.Height,
                        32,
                        bitmap.Width * 4,
                        SDL.SDL_PIXELFORMAT_ABGR8888
                    );

                    if (surface == IntPtr.Zero)
                        throw new Exception($"Failed to create SDL surface: {SDL.SDL_GetError()}");
                    
                    // Create texture from surface pixels
                    var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);
                    SDL.SDL_FreeSurface(surface);

                    if (texture == IntPtr.Zero)
                        throw new Exception($"Failed to create texture: {SDL.SDL_GetError()}");

                    return new(texture, bitmap.Width, bitmap.Height);
                }
            }
        }

        public void Dispose()
        {
            SDL.SDL_DestroyWindow(_window);
        }
    }
}

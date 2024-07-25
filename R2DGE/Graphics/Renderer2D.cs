
using SDL2;
using SkiaSharp;

namespace R2DGE.Graphics
{
    public class Renderer2D : IDisposable
    {
        private IntPtr _renderer;

        public Renderer2D(IntPtr renderPtr)
        {
            _renderer = renderPtr;
        }

        public void Update() => SDL.SDL_RenderPresent(_renderer);
        public void Clear() => SDL.SDL_RenderClear(_renderer);
        public void SetDrawColor(SKColor color) => SDL.SDL_SetRenderDrawColor(_renderer, color.Red, color.Green, color.Blue, color.Alpha);

        public void Dispose()
        {
            SDL.SDL_DestroyRenderer(_renderer);
        }
    }
}

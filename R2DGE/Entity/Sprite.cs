
using R2DGE.Entity.Component;
using R2DGE.NumericExt;
using R2DGE.Texture;
using SDL2;

namespace R2DGE.Entity
{
    public class Sprite : GameObject, IDisposable
    {
        public sealed override string Name => "Sprite";

        IntVector2 _pos = new(0, 0);
        public sealed override IntVector2 Location {
            get { return _pos; }
            set { _pos = value; } 
        }

        private Texture2D _texture;
        private IntPtr _window;
        private IntPtr _renderer;
        public Sprite(IntPtr windowPtr, IntPtr renderPtr, Texture2D texture)
        {
            _window = windowPtr;
            _renderer = renderPtr;
            _texture = texture;
        }

        //TODO: Implement the ability to apply a Source Rect
        public void Draw()
        {
            SDL.SDL_Rect destRect = new() { x = _pos.X, y = _pos.Y, w = _texture.Width, h = _texture.Height };
            SDL.SDL_RenderCopy(_renderer, _texture.TexturePointer, IntPtr.Zero, ref destRect);
        }

        public void Dispose()
        {
            SDL.SDL_DestroyTexture(_texture.TexturePointer);
        }
    }
}

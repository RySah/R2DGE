using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2DGE.Texture
{
    public record struct Texture2D(IntPtr TexturePointer, int Width, int Height);
}

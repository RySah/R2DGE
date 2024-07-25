using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2DGE.NumericExt
{
    public struct IntRect
    {
        public int X = 0;
        public int Y = 0;
        public int Width = 0;
        public int Height = 0;
        public IntRect() { }
        public IntRect(int x, int y, int width, int height) { X = x; Y = y; Width = width; Height = height; }
        public IntRect(IntVector2 pos, int width, int height) { X = pos.X; Y = pos.Y; Width = width; Height = height; }


    }
}

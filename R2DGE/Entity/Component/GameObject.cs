
using R2DGE.NumericExt;
using System.Numerics;

namespace R2DGE.Entity.Component
{
    public abstract class GameObject
    {
        public abstract string Name { get; }
        public abstract IntVector2 Location { get; set; }

    }
}

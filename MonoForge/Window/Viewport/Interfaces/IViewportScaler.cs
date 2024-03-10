using Microsoft.Xna.Framework;

namespace MonoForge;

public interface IViewportScaler
{
    public Point GetSize(Point windowResolution);
}
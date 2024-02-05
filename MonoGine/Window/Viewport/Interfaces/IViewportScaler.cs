using Microsoft.Xna.Framework;

namespace MonoGine;

public interface IViewportScaler
{
    public Point GetSize(Point windowResolution);
}
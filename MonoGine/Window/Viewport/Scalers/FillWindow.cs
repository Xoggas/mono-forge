using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class FillWindow : IViewportScaler
{
    public Point GetScale(Point windowResolution)
    {
        return windowResolution;
    }
}
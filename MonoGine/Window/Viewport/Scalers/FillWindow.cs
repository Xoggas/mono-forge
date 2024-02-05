using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class FillWindow : IViewportScaler
{
    public Point GetSize(Point windowResolution)
    {
        return windowResolution;
    }
}
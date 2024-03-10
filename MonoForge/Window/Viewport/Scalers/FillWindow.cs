using Microsoft.Xna.Framework;

namespace MonoForge;

public sealed class FillWindow : IViewportScaler
{
    public Point GetSize(Point windowResolution)
    {
        return windowResolution;
    }
}
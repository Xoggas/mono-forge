using Microsoft.Xna.Framework;

namespace MonoForge;

public sealed class FitHeight : IViewportScaler
{
    public float AspectRatio { get; set; } = 16f / 9f;

    public Point GetSize(Point windowResolution)
    {
        var width = (int)(windowResolution.Y * AspectRatio);
        var height = windowResolution.Y;
        return new Point(width, height);
    }
}
using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class FitWidth : IViewportScaler
{
    public float AspectRatio { get; set; } = 16f / 9f;

    public Point GetScale(Point windowResolution)
    {
        var width = windowResolution.X;
        var height = (int)(windowResolution.X / AspectRatio);
        return new Point(width, height);
    }
}
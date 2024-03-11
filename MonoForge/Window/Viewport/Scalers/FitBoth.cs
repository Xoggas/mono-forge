using Microsoft.Xna.Framework;

namespace MonoForge;

public sealed class FitBoth : IViewportScaler
{
    private float _aspectRatio = 16f / 9f;

    public float AspectRatio
    {
        get => _aspectRatio;
        set
        {
            _aspectRatio = value;
            _fitHeight.AspectRatio = value;
            _fitWidth.AspectRatio = value;
        }
    }

    private readonly FitHeight _fitHeight = new();
    private readonly FitWidth _fitWidth = new();

    public Point GetSize(Point windowResolution)
    {
        Point fitHeightResolution = _fitHeight.GetSize(windowResolution);
        Point fitWidthResolution = _fitWidth.GetSize(windowResolution);
        return fitHeightResolution.X > fitWidthResolution.X ? fitWidthResolution : fitHeightResolution;
    }
}
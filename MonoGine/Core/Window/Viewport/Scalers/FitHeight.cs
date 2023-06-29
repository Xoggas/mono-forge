using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public sealed class FitHeight : IViewportScaler
{
    public float AspectRatio { get; set; } = 16f / 9f;

    public void Rescale(GraphicsDevice graphicsDevice, IViewport viewport, Point windowResolution)
    {
        int width = (int)(windowResolution.Y * AspectRatio);
        int height = windowResolution.Y;

        viewport.Target.SetSize(graphicsDevice, width, height);
    }
}

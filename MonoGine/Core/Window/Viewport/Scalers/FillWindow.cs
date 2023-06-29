using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public sealed class FillWindow : IViewportScaler
{
    public void Rescale(GraphicsDevice graphicsDevice, IViewport viewport, Point windowResolution)
    {
        viewport.Target.SetSize(graphicsDevice, windowResolution.X, windowResolution.Y);
    }
}

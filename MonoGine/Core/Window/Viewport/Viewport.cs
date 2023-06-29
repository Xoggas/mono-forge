using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public sealed class Viewport : IViewport
{
    internal Viewport(Window window, GraphicsDevice graphicsDevice)
    {
        Target = new RenderTarget(graphicsDevice, window.Width, window.Height);
        Scaler = new FillWindow();
    }

    public RenderTarget Target { get; }
    public IViewportScaler Scaler { get; set; }
    public int Width => Target.Width;
    public int Height => Target.Height;

    public void Dispose()
    {
        Target.Dispose();
    }
}

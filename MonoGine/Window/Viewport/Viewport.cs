using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine;

public sealed class Viewport : IViewport
{
    public RenderTarget Target { get; }
    public Mesh Mesh { get; }
    public IViewportScaler Scaler { get; set; }
    public int Width => Target.Width;
    public int Height => Target.Height;

    internal Viewport(Window window, GraphicsDevice graphicsDevice)
    {
        Target = new RenderTarget(graphicsDevice, window.Width, window.Height);
        Scaler = new FillWindow();
        Mesh = Mesh.NewQuad;
    }

    public void Rescale(GraphicsDevice graphicsDevice, Point windowResolution)
    {
        Target.SetSize(graphicsDevice, Scaler.GetScale(windowResolution));
    }

    public void Dispose()
    {
        Target.Dispose();
    }
}
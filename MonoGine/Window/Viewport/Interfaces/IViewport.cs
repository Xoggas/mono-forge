using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine;

public interface IViewport : IObject
{
    public void Rescale(GraphicsDevice graphicsDevice, Point windowResolution);
    public RenderTarget Target { get; }
    public Mesh Mesh { get; }
    public IViewportScaler Scaler { get; set; }
    public int Width { get; }
    public int Height { get; }
}
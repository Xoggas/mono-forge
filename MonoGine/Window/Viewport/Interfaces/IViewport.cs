using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public interface IViewport : IObject, IDrawable
{
    public RenderTarget2D RenderTarget { get; }
    public IViewportScaler Scaler { get; set; }
    public int Width { get; }
    public int Height { get; }
}
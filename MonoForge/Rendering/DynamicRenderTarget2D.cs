using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class DynamicRenderTarget2D : IObject
{
    private RenderTarget2D _renderTarget;

    internal DynamicRenderTarget2D(GraphicsDevice graphicsDevice, int width, int height)
    {
        _renderTarget = new RenderTarget2D(graphicsDevice, width, height);
    }

    public int Width => _renderTarget.Width;
    public int Height => _renderTarget.Height;

    public static implicit operator RenderTarget2D(DynamicRenderTarget2D a)
    {
        return a._renderTarget;
    }

    public static implicit operator Texture2D(DynamicRenderTarget2D a)
    {
        return a._renderTarget;
    }

    public void Dispose()
    {
        _renderTarget.Dispose();
    }

    internal void SetSize(GraphicsDevice graphicsDevice, Point size)
    {
        _renderTarget.Dispose();
        _renderTarget = new RenderTarget2D(graphicsDevice, size.X, size.Y);
    }
}
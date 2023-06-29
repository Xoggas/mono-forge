using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public sealed class RenderTarget : IObject
{
    private RenderTarget2D _target;

    internal RenderTarget(GraphicsDevice graphicsDevice, int width, int height)
    {
        _target = new RenderTarget2D(graphicsDevice, width, height);
    }
    
    public int Width => _target.Width;
    public int Height => _target.Height;

    public static implicit operator RenderTarget2D?(RenderTarget? a)
    {
        return a?._target;
    }

    public static implicit operator Texture2D(RenderTarget a)
    {
        return a._target;
    }

    public void Dispose()
    {
        _target.Dispose();
    }

    internal void SetSize(GraphicsDevice graphicsDevice, int width, int height)
    {
        _target.Dispose();
        _target = new RenderTarget2D(graphicsDevice, width, height);
    }
}

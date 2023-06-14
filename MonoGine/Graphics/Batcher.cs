using Microsoft.Xna.Framework;

namespace MonoGine.Graphics;

public sealed class Batcher : IBatcher
{
    public void Clear(Engine engine)
    {
        engine.GraphicsDevice.Clear(Color.CornflowerBlue);
    }

    public void Draw(Engine engine)
    {
        
    }

    public void Dispose()
    {
        
    }
}

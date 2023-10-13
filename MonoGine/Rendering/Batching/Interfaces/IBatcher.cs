using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public interface IBatcher : IObject
{
    public void Begin(IEngine engine, Matrix? transformMatrix);
    public void Push(BatchItem batchItem);
    public void End(IEngine engine);
    public void Flush(IEngine engine, Texture2D texture, Shader? shader, int length);
}
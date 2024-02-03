using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public interface IBatcher
{
    public void Push(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public bool TryGetPass(out BatchPassResult batchPassResult);
    public void Reset();
}
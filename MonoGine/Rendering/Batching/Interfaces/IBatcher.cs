using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public interface IBatcher
{
    public void Push(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public IEnumerable<BatchPassResult> GetPasses();
    public void Reset();
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering.Batching;

public interface IRenderQueue : IDisposable
{
    public void Clear(GameBase gameBase, Color color);
    public void Clear(GameBase gameBase, ClearOptions clearOptions, Color color, float depth, int stencil);
    public void SetBatcher(IBatcher batcher);
    public void SetRenderTarget(GameBase gameBase, RenderTarget2D? target);
    public void Begin(GameBase gameBase, RenderConfig config, Matrix? transformMatrix = null);
    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public void End(GameBase gameBase);
}
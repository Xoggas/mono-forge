using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering.Batching;

public interface IRenderQueue : IDisposable
{
    public void Clear(IGame game, Color color);
    public void Clear(IGame game, ClearOptions clearOptions, Color color, float depth, int stencil);
    public void SetBatcher(IBatcher batcher);
    public void SetRenderTarget(IGame game, RenderTarget2D? target);
    public void Begin(IGame game, RenderConfig config, Matrix? transformMatrix = null);
    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public void End(IGame game);
}
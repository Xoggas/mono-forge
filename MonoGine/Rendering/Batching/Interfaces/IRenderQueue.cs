using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public interface IRenderQueue : IDisposable
{
    public void Clear(IEngine engine, Color color);
    public void Clear(IEngine engine, ClearOptions clearOptions, Color color, float depth, int stencil);
    public void SetBatcher(IBatcher batcher);
    public void SetRenderTarget(IEngine engine, RenderTarget2D? target);
    public void Begin(IEngine engine, RenderConfig config, Matrix? transformMatrix = null);
    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public void End(IEngine engine);
}
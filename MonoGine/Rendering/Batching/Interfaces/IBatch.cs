using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public interface IBatch : IObject
{
    public void Clear(IEngine engine, Color color);
    public void Clear(IEngine engine, ClearOptions clearOptions, Color color, float depth, int stencil);
    public void SetRenderTarget(IEngine engine, RenderTarget? target);

    public void Begin(IEngine engine, BlendState? blendState = null, SamplerState? samplerState = null,
        DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null,
        Matrix? transformMatrix = null);

    public void DrawTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth);
    public void End(IEngine engine);
}
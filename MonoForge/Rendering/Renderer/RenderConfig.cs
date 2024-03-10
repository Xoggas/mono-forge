using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class RenderConfig
{
    public static RenderConfig Default => new()
    {
        BlendState = BlendState.NonPremultiplied,
        SamplerState = SamplerState.LinearClamp,
        DepthStencilState = DepthStencilState.None,
        RasterizerState = RasterizerState.CullCounterClockwise
    };

    public BlendState? BlendState;
    public SamplerState? SamplerState;
    public DepthStencilState? DepthStencilState;
    public RasterizerState? RasterizerState;
}
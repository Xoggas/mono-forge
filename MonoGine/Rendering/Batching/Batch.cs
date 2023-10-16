using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public sealed class Batch : IBatch
{
    private readonly IBatcher _batcher;
    private bool _batchHasBegun;

    public Batch(IEngine engine)
    {
        _batcher = new Batcher(engine);
    }

    public void Clear(IEngine engine, Color color)
    {
        engine.GraphicsDevice.Clear(color);
    }

    public void Clear(IEngine engine, ClearOptions clearOptions, Color color, float depth, int stencil)
    {
        engine.GraphicsDevice.Clear(clearOptions, color, depth, stencil);
    }

    public void SetRenderTarget(IEngine engine, RenderTarget? target)
    {
        engine.GraphicsDevice.SetRenderTarget(target);
    }

    public void Begin(IEngine engine, BlendState? blendState = null, SamplerState? samplerState = null,
        DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null,
        Matrix? transformMatrix = null)
    {
        if (_batchHasBegun)
        {
            throw new InvalidOperationException("Another batch wasn't finished!");
        }

        engine.GraphicsDevice.BlendState = blendState ?? BlendState.NonPremultiplied;
        engine.GraphicsDevice.SamplerStates[0] = samplerState ?? SamplerState.LinearClamp;
        engine.GraphicsDevice.DepthStencilState = depthStencilState ?? DepthStencilState.None;
        engine.GraphicsDevice.RasterizerState = rasterizerState ?? RasterizerState.CullCounterClockwise;

        _batcher.Begin(engine, transformMatrix);
        _batchHasBegun = true;
    }

    public void DrawTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _batcher.Push(new BatchItem(texture, mesh, shader, depth));
    }

    public void End(IEngine engine)
    {
        _batcher.End(engine);
        _batchHasBegun = false;
    }

    public void Dispose()
    {
        _batcher.Dispose();
    }
}
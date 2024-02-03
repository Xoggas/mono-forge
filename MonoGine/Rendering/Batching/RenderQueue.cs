using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public sealed class RenderQueue : IRenderQueue
{
    private readonly SpriteEffect _spriteEffect;
    private readonly EffectPass _effectPass;
    private IBatcher _batcher;
    private bool _batchHasBegun;

    public RenderQueue(IEngine engine, IBatcher batcher)
    {
        _spriteEffect = new SpriteEffect(engine.GraphicsDevice);
        _effectPass = _spriteEffect.CurrentTechnique.Passes[0];
        _batcher = batcher;
    }

    public void Clear(IEngine engine, Color color)
    {
        engine.GraphicsDevice.Clear(color);
    }

    public void Clear(IEngine engine, ClearOptions clearOptions, Color color, float depth, int stencil)
    {
        engine.GraphicsDevice.Clear(clearOptions, color, depth, stencil);
    }

    public void SetBatcher(IBatcher batcher)
    {
        _batcher = batcher;
    }

    public void SetRenderTarget(IEngine engine, RenderTarget? target)
    {
        engine.GraphicsDevice.SetRenderTarget(target);
    }

    public void Begin(IEngine engine, RenderConfig renderConfig, Matrix? transformMatrix = null)
    {
        if (_batchHasBegun)
        {
            throw new InvalidOperationException("Another batch wasn't finished!");
        }

        _batcher.Reset();
        _spriteEffect.TransformMatrix = transformMatrix;
        _effectPass.Apply();

        engine.GraphicsDevice.BlendState = renderConfig.BlendState ?? BlendState.NonPremultiplied;
        engine.GraphicsDevice.SamplerStates[0] = renderConfig.SamplerState ?? SamplerState.LinearClamp;
        engine.GraphicsDevice.DepthStencilState = renderConfig.DepthStencilState ?? DepthStencilState.None;
        engine.GraphicsDevice.RasterizerState = renderConfig.RasterizerState ?? RasterizerState.CullCounterClockwise;

        _batchHasBegun = true;
    }

    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _batcher.Push(texture, mesh, shader, depth);
    }

    public void End(IEngine engine)
    {
        while (_batcher.TryGetPass(out BatchPassResult batchedPass))
        {
            DrawUtility.Draw(engine.GraphicsDevice, batchedPass);
        }

        _batchHasBegun = false;
    }

    public void Dispose()
    {
        _spriteEffect.Dispose();
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering.Batching;

public sealed class RenderQueue : IRenderQueue
{
    private readonly SpriteEffect _spriteEffect;
    private readonly EffectPass _effectPass;
    private IDrawingService _drawingService;
    private IBatcher _batcher;
    private bool _batchHasBegun;

    public RenderQueue(GameBase gameBase, IBatcher batcher, IDrawingService drawingService)
    {
        _spriteEffect = new SpriteEffect(gameBase.GraphicsDevice);
        _effectPass = _spriteEffect.CurrentTechnique.Passes[0];
        _batcher = batcher;
        _drawingService = drawingService;
    }

    public void Clear(GameBase gameBase, Color color)
    {
        gameBase.GraphicsDevice.Clear(color);
    }

    public void Clear(GameBase gameBase, ClearOptions clearOptions, Color color, float depth, int stencil)
    {
        gameBase.GraphicsDevice.Clear(clearOptions, color, depth, stencil);
    }

    public void SetBatcher(IBatcher batcher)
    {
        _batcher = batcher;
    }

    public void SetRenderTarget(GameBase gameBase, RenderTarget2D? target)
    {
        gameBase.GraphicsDevice.SetRenderTarget(target);
    }

    public void Begin(GameBase gameBase, RenderConfig renderConfig, Matrix? transformMatrix = null)
    {
        if (_batchHasBegun)
        {
            throw new InvalidOperationException("Another batch wasn't finished!");
        }

        _batcher.Reset();
        _spriteEffect.TransformMatrix = transformMatrix;
        _effectPass.Apply();

        gameBase.GraphicsDevice.BlendState = renderConfig.BlendState ?? BlendState.NonPremultiplied;
        gameBase.GraphicsDevice.SamplerStates[0] = renderConfig.SamplerState ?? SamplerState.LinearClamp;
        gameBase.GraphicsDevice.DepthStencilState = renderConfig.DepthStencilState ?? DepthStencilState.None;
        gameBase.GraphicsDevice.RasterizerState = renderConfig.RasterizerState ?? RasterizerState.CullCounterClockwise;

        _batchHasBegun = true;
    }

    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _batcher.Push(texture, mesh, shader, depth);
    }

    public void End(GameBase gameBase)
    {
        foreach (BatchPassResult pass in _batcher.GetPasses())
        {
            _drawingService.DrawMeshes(gameBase.GraphicsDevice, pass);
        }

        _batchHasBegun = false;
    }

    public void Dispose()
    {
        _spriteEffect.Dispose();
    }
}
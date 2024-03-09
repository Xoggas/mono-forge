using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public sealed class RenderQueue : IRenderQueue
{
    private readonly SpriteEffect _spriteEffect;
    private readonly EffectPass _effectPass;
    private IDrawingService _drawingService;
    private IBatcher _batcher;
    private bool _batchHasBegun;

    public RenderQueue(IGame game, IBatcher batcher, IDrawingService drawingService)
    {
        _spriteEffect = new SpriteEffect(game.GraphicsDevice);
        _effectPass = _spriteEffect.CurrentTechnique.Passes[0];
        _batcher = batcher;
        _drawingService = drawingService;
    }

    public void Clear(IGame game, Color color)
    {
        game.GraphicsDevice.Clear(color);
    }

    public void Clear(IGame game, ClearOptions clearOptions, Color color, float depth, int stencil)
    {
        game.GraphicsDevice.Clear(clearOptions, color, depth, stencil);
    }

    public void SetBatcher(IBatcher batcher)
    {
        _batcher = batcher;
    }

    public void SetRenderTarget(IGame game, RenderTarget2D? target)
    {
        game.GraphicsDevice.SetRenderTarget(target);
    }

    public void Begin(IGame game, RenderConfig renderConfig, Matrix? transformMatrix = null)
    {
        if (_batchHasBegun)
        {
            throw new InvalidOperationException("Another batch wasn't finished!");
        }

        _batcher.Reset();
        _spriteEffect.TransformMatrix = transformMatrix;
        _effectPass.Apply();

        game.GraphicsDevice.BlendState = renderConfig.BlendState ?? BlendState.NonPremultiplied;
        game.GraphicsDevice.SamplerStates[0] = renderConfig.SamplerState ?? SamplerState.LinearClamp;
        game.GraphicsDevice.DepthStencilState = renderConfig.DepthStencilState ?? DepthStencilState.None;
        game.GraphicsDevice.RasterizerState = renderConfig.RasterizerState ?? RasterizerState.CullCounterClockwise;

        _batchHasBegun = true;
    }

    public void EnqueueTexturedMesh(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _batcher.Push(texture, mesh, shader, depth);
    }

    public void End(IGame game)
    {
        foreach (BatchPassResult pass in _batcher.GetPasses())
        {
            _drawingService.DrawMeshes(game.GraphicsDevice, pass);
        }

        _batchHasBegun = false;
    }

    public void Dispose()
    {
        _spriteEffect.Dispose();
    }
}
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public sealed class CustomRenderer : IRenderer
{
    private readonly IBatch _batch;
    private readonly RasterizerState _rasterizerState;

    public CustomRenderer(IEngine engine)
    {
        _batch = new Batch(engine);
        _rasterizerState = new RasterizerState
        {
            CullMode = CullMode.CullCounterClockwiseFace,
            MultiSampleAntiAlias = true
        };
    }

    public void Draw(IEngine engine, IScene scene)
    {
        DrawScene(engine, scene);
    }

    public void Dispose()
    {
        _batch.Dispose();
    }

    private void DrawScene(IEngine engine, IScene scene)
    {
        _batch.SetRenderTarget(engine, null);
        _batch.Clear(engine, scene.Camera.BackgroundColor);
        _batch.Begin(engine, transformMatrix: scene.Camera.TransformMatrix, rasterizerState: _rasterizerState,
            blendState: BlendState.NonPremultiplied);

        scene.Root.Draw(engine, _batch);
        scene.Canvas.Draw(engine, _batch);

        _batch.End(engine);
    }
}
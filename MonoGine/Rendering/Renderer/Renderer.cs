using Microsoft.Xna.Framework;
using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

//TODO: Implement culling
public sealed class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;

    public Renderer(IEngine engine, IBatcher batcher, IDrawingService drawingService, RenderConfig config)
    {
        Config = config;
        _renderQueue = new RenderQueue(engine, batcher, drawingService);
    }

    public RenderConfig Config { get; set; }

    public void SetBatcher(IBatcher batcher)
    {
        _renderQueue.SetBatcher(batcher);
    }

    public void Draw(IEngine engine, Scene scene)
    {
        DrawScene(engine, scene);
        DrawViewport(engine, engine.Window.Viewport);
    }

    private void DrawScene(IEngine engine, Scene scene)
    {
        _renderQueue.SetRenderTarget(engine, engine.Window.Viewport.RenderTarget);
        _renderQueue.Clear(engine, scene.Camera.BackgroundColor);
        _renderQueue.Begin(engine, Config, scene.Camera.TransformMatrix);

        scene.Draw(engine, _renderQueue);

        _renderQueue.End(engine);
    }

    private void DrawViewport(IEngine engine, IDrawable viewport)
    {
        _renderQueue.SetRenderTarget(engine, null);
        _renderQueue.Clear(engine, Color.Black);
        _renderQueue.Begin(engine, Config);

        viewport.Draw(engine, _renderQueue);

        _renderQueue.End(engine);
    }
}
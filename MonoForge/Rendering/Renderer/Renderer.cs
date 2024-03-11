using Microsoft.Xna.Framework;
using MonoForge.Rendering.Batching;
using MonoForge.SceneManagement;

namespace MonoForge.Rendering;

//TODO: Implement culling
public sealed class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;

    public Renderer(GameBase gameBase, IBatcher batcher, IDrawingService drawingService, RenderConfig config)
    {
        Config = config;
        _renderQueue = new RenderQueue(gameBase, batcher, drawingService);
    }

    public RenderConfig Config { get; set; }

    public void SetBatcher(IBatcher batcher)
    {
        _renderQueue.SetBatcher(batcher);
    }

    public void Draw(GameBase gameBase, Scene scene)
    {
        DrawScene(gameBase, scene);
        DrawViewport(gameBase, gameBase.Window.Viewport);
    }

    private void DrawScene(GameBase gameBase, Scene scene)
    {
        _renderQueue.SetRenderTarget(gameBase, gameBase.Window.Viewport.RenderTarget);
        _renderQueue.Clear(gameBase, scene.Camera.BackgroundColor);
        _renderQueue.Begin(gameBase, Config, scene.Camera.TransformMatrix);

        scene.Draw(gameBase, _renderQueue);

        _renderQueue.End(gameBase);
    }

    private void DrawViewport(GameBase gameBase, IDrawable viewport)
    {
        _renderQueue.SetRenderTarget(gameBase, null);
        _renderQueue.Clear(gameBase, Color.Black);
        _renderQueue.Begin(gameBase, Config);

        viewport.Draw(gameBase, _renderQueue);

        _renderQueue.End(gameBase);
    }
}
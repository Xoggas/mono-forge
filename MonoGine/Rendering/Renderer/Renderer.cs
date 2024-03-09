using Microsoft.Xna.Framework;
using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

//TODO: Implement culling
public sealed class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;

    public Renderer(IGame game, IBatcher batcher, IDrawingService drawingService, RenderConfig config)
    {
        Config = config;
        _renderQueue = new RenderQueue(game, batcher, drawingService);
    }

    public RenderConfig Config { get; set; }

    public void SetBatcher(IBatcher batcher)
    {
        _renderQueue.SetBatcher(batcher);
    }

    public void Draw(IGame game, Scene scene)
    {
        DrawScene(game, scene);
        DrawViewport(game, game.Window.Viewport);
    }

    private void DrawScene(IGame game, Scene scene)
    {
        _renderQueue.SetRenderTarget(game, game.Window.Viewport.RenderTarget);
        _renderQueue.Clear(game, scene.Camera.BackgroundColor);
        _renderQueue.Begin(game, Config, scene.Camera.TransformMatrix);

        scene.Draw(game, _renderQueue);

        _renderQueue.End(game);
    }

    private void DrawViewport(IGame game, IDrawable viewport)
    {
        _renderQueue.SetRenderTarget(game, null);
        _renderQueue.Clear(game, Color.Black);
        _renderQueue.Begin(game, Config);

        viewport.Draw(game, _renderQueue);

        _renderQueue.End(game);
    }
}
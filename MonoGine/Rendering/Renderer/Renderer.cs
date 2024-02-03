using Microsoft.Xna.Framework;
using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public sealed class Renderer : IRenderer
{
    private readonly IRenderQueue _renderQueue;

    public Renderer(IEngine engine, IBatcher batcher, RenderConfig config)
    {
        Config = config;
        _renderQueue = new RenderQueue(engine, batcher);
    }

    public RenderConfig Config { get; set; }

    public void SetBatcher(IBatcher batcher)
    {
        _renderQueue.SetBatcher(batcher);
    }

    public void Draw(IEngine engine, IScene scene)
    {
        DrawScene(engine, scene);
        DrawViewport(engine, engine.Window.Viewport);
    }

    private void DrawScene(IEngine engine, IScene scene)
    {
        _renderQueue.SetRenderTarget(engine, engine.Window.Viewport.Target);
        _renderQueue.Clear(engine, scene.Camera.BackgroundColor);
        _renderQueue.Begin(engine, Config, scene.Camera.TransformMatrix);

        scene.Draw(engine, _renderQueue);

        _renderQueue.End(engine);
    }

    private void DrawViewport(IEngine engine, IViewport viewport)
    {
        _renderQueue.SetRenderTarget(engine, null);
        _renderQueue.Clear(engine, Color.Black);
        _renderQueue.Begin(engine, Config);

        ApplyMatrixToViewport(engine.Window, engine.Window.Viewport);

        _renderQueue.EnqueueTexturedMesh(viewport.Target, viewport.Mesh, null, 0f);
        _renderQueue.End(engine);
    }

    private void ApplyMatrixToViewport(Window window, IViewport viewport)
    {
        Matrix matrix = Matrix.CreateScale(viewport.Width, viewport.Height, 0) *
                        Matrix.CreateTranslation(new Vector3(window.Width, window.Height, 0) * 0.5f);

        viewport.Mesh.Vertices[0] = new Vertex(Vector3.Transform(Vector3.Zero, matrix), Color.White);
        viewport.Mesh.Vertices[1] = new Vertex(Vector3.Transform(Vector3.UnitY, matrix), Color.White);
        viewport.Mesh.Vertices[2] = new Vertex(Vector3.Transform(Vector3.UnitX, matrix), Color.White);
        viewport.Mesh.Vertices[3] = new Vertex(Vector3.Transform(Vector3.One, matrix), Color.White);
    }
}
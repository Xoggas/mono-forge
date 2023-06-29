using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public sealed class CustomRenderer : IRenderer
{
    private readonly IBatch _batch;

    public CustomRenderer(IEngine engine)
    {
        _batch = new Batch(engine);
    }

    public Shader? PostProcessingEffect { get; set; }

    public void Draw(IEngine engine, IScene scene)
    {
        DrawScene(engine, scene);
        DrawViewport(engine, engine.Window.Viewport);
    }

    public void Dispose()
    {
        _batch.Dispose();
    }

    private void DrawScene(IEngine engine, IScene scene)
    {
        _batch.SetRenderTarget(engine, engine.Window.Viewport.Target);
        _batch.Clear(engine, scene.Camera.BackgroundColor);
        _batch.Begin(engine, transformMatrix: GetCameraMatrix(scene.Camera, engine.Window.Viewport), blendState: BlendState.NonPremultiplied);

        scene.Root.Draw(engine, _batch);
        scene.Canvas.Draw(engine, _batch);

        _batch.End(engine);
    }

    private void DrawViewport(IEngine engine, IViewport viewport)
    {
        _batch.SetRenderTarget(engine, null);
        _batch.Clear(engine, Color.Black);
        _batch.Begin(engine);
        _batch.DrawSprite(viewport.Target, Color.White, GetViewportMatrix(engine.Window, viewport), Vector2.One * 0.5f, PostProcessingEffect, null, 0f);
        _batch.End(engine);
    }

    private Matrix GetCameraMatrix(ICamera camera, IViewport viewport)
    {
        return Matrix.CreateScale(viewport.Width / 1280f, viewport.Height / 720f, 1f) * camera.TransformMatrix;
    }

    private Matrix GetViewportMatrix(Window window, IViewport viewport)
    {
        return Matrix.CreateScale(viewport.Width, viewport.Height, 0) * Matrix.CreateTranslation(new Vector3(window.Width, window.Height, 0) * 0.5f);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;
using MonoGine.Rendering.Batching;

namespace MonoGine;

public sealed class Viewport : IViewport
{
    public RenderTarget2D RenderTarget => _dynamicRenderTarget;
    public IViewportScaler Scaler { get; set; } = new FillWindow();
    public int Width => RenderTarget.Width;
    public int Height => RenderTarget.Height;

    private readonly Mesh _mesh;
    private readonly DynamicRenderTarget2D _dynamicRenderTarget;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly Window _window;

    internal Viewport(Window window, GraphicsDevice graphicsDevice)
    {
        _dynamicRenderTarget = new DynamicRenderTarget2D(graphicsDevice, window.Width, window.Height);
        _window = window;
        _graphicsDevice = graphicsDevice;
        _window.ResolutionChanged += OnWindowResolutionChanged;
        _mesh = Mesh.NewQuad;
    }

    public void Dispose()
    {
        RenderTarget.Dispose();
        _window.ResolutionChanged -= OnWindowResolutionChanged;
    }

    public void Draw(IEngine engine, IRenderQueue renderQueue)
    {
        renderQueue.EnqueueTexturedMesh(_dynamicRenderTarget, _mesh, null, 0f);
    }

    private void OnWindowResolutionChanged(Point windowResolution)
    {
        ResizeRenderTarget(windowResolution);
        RecalculateViewportMesh(windowResolution);
    }

    private void ResizeRenderTarget(Point windowResolution)
    {
        _dynamicRenderTarget.SetSize(_graphicsDevice, Scaler.GetSize(windowResolution));
    }

    private void RecalculateViewportMesh(Point windowResolution)
    {
        Vector3 pivot = new(0.5f, 0.5f, 0f);
        Vector3 screenCenter = new Vector3(windowResolution.ToVector2(), 0f) * 0.5f;
        Vector3 screenSize = new(Scaler.GetSize(windowResolution).ToVector2(), 0f);
        Matrix transformMatrix = Matrix.CreateScale(screenSize) * Matrix.CreateTranslation(screenCenter);

        _mesh.Vertices[0] = new Vertex(Vector3.Transform(Vector3.Zero - pivot, transformMatrix), Color.White);
        _mesh.Vertices[1] = new Vertex(Vector3.Transform(Vector3.UnitY - pivot, transformMatrix), Color.White);
        _mesh.Vertices[2] = new Vertex(Vector3.Transform(Vector3.UnitX - pivot, transformMatrix), Color.White);
        _mesh.Vertices[3] = new Vertex(Vector3.Transform(Vector3.One - pivot, transformMatrix), Color.White);
    }
}
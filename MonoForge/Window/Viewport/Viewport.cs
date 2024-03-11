using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoForge.Extensions;
using MonoForge.Rendering;
using MonoForge.Rendering.Batching;

namespace MonoForge;

public sealed class Viewport : IDrawable, IDisposable
{
    public IViewportScaler Scaler
    {
        get => _scaler;
        set
        {
            _scaler = value;
            SetResolution(Resolution);
        }
    }

    public RenderTarget2D RenderTarget => _dynamicRenderTarget;
    public Point Resolution => new(RenderTarget.Width, RenderTarget.Height);

    private readonly Mesh _mesh;
    private readonly DynamicRenderTarget2D _dynamicRenderTarget;
    private readonly GraphicsDevice _graphicsDevice;
    private readonly Window _window;
    private IViewportScaler _scaler;

    internal Viewport(Window window, GraphicsDevice graphicsDevice)
    {
        _window = window;
        _window.ResolutionChanged += SetResolution;
        _dynamicRenderTarget = new DynamicRenderTarget2D(graphicsDevice, window.Width, window.Height);
        _graphicsDevice = graphicsDevice;
        _mesh = Mesh.NewQuad;
        _scaler = new FillWindow();
    }

    public void Dispose()
    {
        RenderTarget.Dispose();
    }

    public void Draw(GameBase gameBase, IRenderQueue renderQueue)
    {
        renderQueue.EnqueueTexturedMesh(_dynamicRenderTarget, _mesh, null, 0f);
    }

    private void SetResolution(Point windowResolution)
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
        Vector3 screenCenter = new(windowResolution.X * 0.5f, windowResolution.Y * 0.5f, 0f);
        var viewportSize = Scaler.GetSize(windowResolution).ToVector3();
        Matrix transformMatrix = Matrix.CreateScale(viewportSize) *
                                 Matrix.CreateTranslation(screenCenter);

        _mesh.Vertices[0] = new Vertex(Vector3.Transform(Vector3.Zero - pivot, transformMatrix), Color.White);
        _mesh.Vertices[1] = new Vertex(Vector3.Transform(Vector3.UnitY - pivot, transformMatrix), Color.White);
        _mesh.Vertices[2] = new Vertex(Vector3.Transform(Vector3.UnitX - pivot, transformMatrix), Color.White);
        _mesh.Vertices[3] = new Vertex(Vector3.Transform(Vector3.One - pivot, transformMatrix), Color.White);
    }
}
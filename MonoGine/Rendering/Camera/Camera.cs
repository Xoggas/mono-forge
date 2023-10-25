using System;
using Microsoft.Xna.Framework;
using MonoGine.Animations;

namespace MonoGine.Rendering;

public sealed class Camera : ICamera
{
    public Color BackgroundColor { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Zoom { get; set; } = 1f;
    public Matrix TransformMatrix { get; private set; }

    public void Update(IEngine engine)
    {
        ClampZoom();
        UpdateMatrix(engine.Window.Viewport);
    }

    public void Dispose()
    {
    }

    private void ClampZoom()
    {
        if (Zoom <= 0f)
        {
            Zoom = 0f;
        }
    }

    private void UpdateMatrix(IViewport viewport)
    {
        var width = viewport.Width;
        var height = viewport.Height;

        TransformMatrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
                          Matrix.CreateTranslation(-width * 0.5f, -height * 0.5f, 0f) *
                          Matrix.CreateRotationZ(MathHelper.ToRadians(Rotation)) *
                          Matrix.CreateScale(Zoom, Zoom, 1f) *
                          Matrix.CreateTranslation(width * 0.5f, height * 0.5f, 0f);
    }

    //TODO: Rework this shit as well
    public string? Name { get; set; }

    //TODO: Rework this method
    public IAnimatable? FindChildByName(string name)
    {
        return null;
    }

    public void SetProperty(string name, float value)
    {
        throw new NotImplementedException();
    }
}
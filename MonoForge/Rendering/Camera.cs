using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoForge.Animations;

namespace MonoForge.Rendering;

public sealed class Camera : IAnimatable
{
    public Color BackgroundColor { get; set; }
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Zoom { get; set; } = 1f;
    public Matrix TransformMatrix { get; private set; }

    public void Update(Point gameResolution, Point viewportResolution)
    {
        ClampZoom();
        UpdateMatrix(viewportResolution.ToVector2() / gameResolution.ToVector2());
    }

    public IAnimatable? GetChild(string name)
    {
        return default;
    }

    public Action<float> GetPropertySetter(string name)
    {
        return name switch
        {
            "backgroundColor.r" => value => BackgroundColor = new Color(value, BackgroundColor.G, BackgroundColor.B),
            "backgroundColor.g" => value => BackgroundColor = new Color(BackgroundColor.R, value, BackgroundColor.B),
            "backgroundColor.b" => value => BackgroundColor = new Color(BackgroundColor.R, BackgroundColor.G, value),
            "pos.x" => value => Position = new Vector2(value, Position.Y),
            "pos.y" => value => Position = new Vector2(Position.X, value),
            "rotation" => value => Rotation = value,
            "zoom" => value => Zoom = value,
            _ => throw new KeyNotFoundException()
        };
    }

    private void ClampZoom()
    {
        if (Zoom <= 0f)
        {
            Zoom = 0f;
        }
    }

    private void UpdateMatrix(Vector2 viewportResolution)
    {
        var width = viewportResolution.X;
        var height = viewportResolution.Y;

        TransformMatrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
                          Matrix.CreateTranslation(-width * 0.5f, -height * 0.5f, 0f) *
                          Matrix.CreateRotationZ(MathHelper.ToRadians(Rotation)) *
                          Matrix.CreateScale(Zoom, Zoom, 1f) *
                          Matrix.CreateTranslation(width * 0.5f, height * 0.5f, 0f);
    }
}
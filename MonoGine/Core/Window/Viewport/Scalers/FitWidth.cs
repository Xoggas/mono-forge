﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public sealed class FitWidth : IViewportScaler
{
    public float AspectRatio { get; set; } = 16f / 9f;

    public void Rescale(GraphicsDevice graphicsDevice, IViewport viewport, Point windowResolution)
    {
        int width = windowResolution.X;
        int height = (int)(windowResolution.X / AspectRatio);

        viewport.Target.SetSize(graphicsDevice, width, height);
    }
}
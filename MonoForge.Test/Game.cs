using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForge.Test;

public sealed class Game : GameBase
{
    private Point _offset;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        Cursor.IsVisible = true;
        SetupWindow();
    }

    protected override void OnStart()
    {
        base.OnStart();
        LoadScene();
    }

    protected override void OnUpdate(GameTime gameTime)
    {
        base.OnUpdate(gameTime);

        if (Input.Keyboard.WasPressed(Keys.NumPad1))
        {
            SceneManager.Load<RenderingTestScene>(this, SceneLoadingArgs.Empty);
        }

        if (Input.Keyboard.WasPressed(Keys.NumPad2))
        {
            SceneManager.Load<AudioTestScene>(this, SceneLoadingArgs.Empty);
        }

        if (Input.Keyboard.WasPressed(Keys.Delete))
        {
            SceneManager.Load<EmptyScene>(this, SceneLoadingArgs.Empty);
        }

        var offset = new Vector2(MathF.Cos(Time.ElapsedTime) * 25f, MathF.Sin(Time.ElapsedTime * 2f) * 25f);

        SceneManager.CurrentScene.Camera.Position = offset;

        Window.Position = new Point(360, 180) + offset.ToPoint();
    }

    private void SetupWindow()
    {
        Window.Title = "MonoForge";
        Window.Resolution = new Point(1280, 720);
        Window.Viewport.Scaler = new FitAspectRatio();
        Window.IsFullscreen = false;
        Window.AllowResizing = true;
        Window.Framerate = 75;
    }

    private void LoadScene()
    {
        SceneManager.Load<RenderingTestScene>(this, SceneLoadingArgs.Empty);
    }
}
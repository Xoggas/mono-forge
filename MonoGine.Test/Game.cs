using Microsoft.Xna.Framework;
using MonoGine.Rendering;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        SetupWindow();
        SetupRenderer();
        SetupCursor();
        LoadScene();
    }

    private void SetupWindow()
    {
        Window.Title = "MonoGine";
        Window.Viewport.Scaler = new FitHeight();
        Window.Resolution = new Point(1280, 720);
        Window.Framerate = 60;
        Window.UseVSync = true;
    }

    private void SetupCursor()
    {
        Cursor.IsVisible = true;
        Cursor.Texture = ResourceManager.LoadFromFile<Sprite>("Cursor.png");
    }

    private void SetupRenderer()
    {
        Renderer.Dispose();
        Renderer = new CustomRenderer(this);
    }

    private void LoadScene()
    {
        SceneManager.Load(this, new MainScene());
    }
}
using Microsoft.Xna.Framework;
using MonoGine.Rendering;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();
        SetupWindow();
        SetupCursor();
        LoadScene();
    }

    private void SetupWindow()
    {
        Window.Title = "MonoGine";
        Window.Viewport.Scaler = new FitHeight();
        Window.Resolution = new Point(1280, 720);
        Window.Framerate = 60;
    }

    private void SetupCursor()
    {
        Cursor.IsVisible = true;
        Cursor.Sprite = AssetManager.LoadFromFile<Sprite>("Cursor.png");
    }

    private void LoadScene()
    {
        SceneManager.Load(this, new RenderingTestScene());
    }
}
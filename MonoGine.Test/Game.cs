using Microsoft.Xna.Framework;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();
        Cursor.IsVisible = true;
        SetupWindow();
        LoadScene();
    }

    private void SetupWindow()
    {
        Window.Title = "MonoGine";
        Window.Viewport.Scaler = new FillWindow();
        Window.Resolution = new Point(1280, 720);
        Window.Framerate = 60;
    }

    private void LoadScene()
    {
        SceneManager.Load(this, new RenderingTestScene());
    }
}
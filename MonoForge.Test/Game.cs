using Microsoft.Xna.Framework;

namespace MonoForge.Test;

public sealed class Game : GameBase
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
        Window.Title = "MonoForge";
        Window.Resolution = new Point(1280, 720);
        Window.Viewport.Scaler = new FitBoth();
        Window.IsFullscreen = false;
        Window.Framerate = 60;
        Window.AllowResizing = true;
    }

    private void LoadScene()
    {
        SceneManager.Load(this, new RenderingTestScene());
    }
}
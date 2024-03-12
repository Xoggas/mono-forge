using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForge.Test;

public sealed class Game : GameBase
{
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
            SceneManager.Load(this, new RenderingTestScene());
        }

        if (Input.Keyboard.WasPressed(Keys.NumPad2))
        {
            SceneManager.Load(this, new AudioTestScene());
        }

        if (Input.Keyboard.WasPressed(Keys.Delete))
        {
            SceneManager.Load(this, new EmptyScene());
        }
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
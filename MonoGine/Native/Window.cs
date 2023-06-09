using Microsoft.Xna.Framework;

namespace MonoGine;

public class Window : Object
{
    private static GameWindow s_gameWindow;
    private static GraphicsDeviceManager s_graphics;

    public Window(GameWindow gameWindow, GraphicsDeviceManager graphics)
    {
        s_gameWindow = gameWindow;
        s_graphics = graphics;
        s_graphics.HardwareModeSwitch = false;
    }

    public static string Title
    {
        get => s_gameWindow.Title;
        set => s_gameWindow.Title = value;
    }

    public static Point Resolution
    {
        get => new Point(s_graphics.PreferredBackBufferWidth, s_graphics.PreferredBackBufferHeight);
        set
        {
            s_graphics.PreferredBackBufferWidth = value.X;
            s_graphics.PreferredBackBufferHeight = value.Y;
            s_graphics.ApplyChanges();
        }
    }

    public static Point Position
    {
        get => s_gameWindow.Position;
        set => s_gameWindow.Position = value;
    }

    public static bool IsFullscreen
    {
        get => s_graphics.IsFullScreen;
        set
        {
            s_graphics.IsFullScreen = value;
            s_graphics.ApplyChanges();
        }
    }

    public override void Dispose()
    {
        s_gameWindow = null;
    }
}

using Microsoft.Xna.Framework;
using System;

namespace MonoGine;

public class Window : Object
{
    private static GameWindow s_gameWindow;
    private static GraphicsDeviceManager s_graphics;
    private static Game s_game;

    public Window(Game game, GameWindow window, GraphicsDeviceManager graphics)
    {
        s_game = game;
        s_gameWindow = window;
        s_graphics = graphics;
        s_graphics.HardwareModeSwitch = false;
    }

    public static string Title
    {
        get => s_gameWindow.Title;
        set => s_gameWindow.Title = value;
    }

    public static bool IsFixedFramerate
    {
        get => s_game.IsFixedTimeStep;
        set => s_game.IsFixedTimeStep = value;
    }

    public static int Framerate
    {
        get => (int)(1d / s_game.TargetElapsedTime.TotalSeconds);
        set => s_game.TargetElapsedTime = TimeSpan.FromSeconds(1d / value);
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
        s_graphics = null;
        s_game = null;
    }
}

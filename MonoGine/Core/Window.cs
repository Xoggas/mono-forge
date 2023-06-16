using Microsoft.Xna.Framework;
using System;

namespace MonoGine;

/// <summary>
/// Represents a window in the game engine.
/// </summary>
public sealed class Window : IObject
{
    private Core _core;
    private Engine _engine;

    internal Window(Core core, Engine engine)
    {
        _core = core;
        _core.GraphicsDeviceManager.HardwareModeSwitch = false;
        _engine = engine;
    }

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    public string Title
    {
        get => _core.Window.Title;
        set => _core.Window.Title = value;
    }

    /// <summary>
    /// Gets or sets the position of the window.
    /// </summary>
    public Point Position
    {
        get => _core.Window.Position;
        set => _core.Window.Position = value;
    }

    /// <summary>
    /// Gets or sets the resolution of the window.
    /// </summary>
    public Point Resolution
    {
        get => new Point(_core.GraphicsDeviceManager.PreferredBackBufferWidth, _core.GraphicsDeviceManager.PreferredBackBufferHeight);
        set
        {
            _core.GraphicsDeviceManager.PreferredBackBufferWidth = value.X;
            _core.GraphicsDeviceManager.PreferredBackBufferHeight = value.Y;
            _core.GraphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets the target framerate of the window.
    /// </summary>
    public int Framerate
    {
        get => (int)Math.Round(1d / _core.TargetElapsedTime.TotalSeconds);
        set => _core.TargetElapsedTime = TimeSpan.FromSeconds(1d / value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is in fullscreen mode.
    /// </summary>
    public bool IsFullscreen
    {
        get => _core.GraphicsDeviceManager.IsFullScreen;
        set
        {
            _core.GraphicsDeviceManager.IsFullScreen = value;
            _core.GraphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    public bool IsBorderless
    {
        get => _core.Window.IsBorderless;
        set => _core.Window.IsBorderless = value;
    }

    /// <summary>
    /// Disposes the window.
    /// </summary>
    public void Dispose()
    {

    }
}

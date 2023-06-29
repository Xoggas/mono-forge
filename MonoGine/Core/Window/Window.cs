using System;
using Microsoft.Xna.Framework;

namespace MonoGine;

/// <summary>
/// Represents a window in the game engine.
/// </summary>
public sealed class Window : IObject
{
    private readonly Core _core;
    private readonly IViewport _viewport;

    internal Window(Core core)
    {
        _core = core;
        _core.GraphicsDeviceManager.HardwareModeSwitch = false;
        _viewport = new Viewport(this, core.GraphicsDevice);
    }

    /// <summary>
    /// Gets the window viewport.
    /// </summary>
    public IViewport Viewport => _viewport;

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
            if (Resolution == value)
            {
                return;
            }

            _core.GraphicsDeviceManager.PreferredBackBufferWidth = value.X;
            _core.GraphicsDeviceManager.PreferredBackBufferHeight = value.Y;
            _core.GraphicsDeviceManager.ApplyChanges();
            
            _viewport.Scaler.Rescale(_core.GraphicsDevice, _viewport, value);
        }
    }

    /// <summary>
    /// Gets current window width.
    /// </summary>
    public int Width => Resolution.X;

    /// <summary>
    /// Gets current window height.
    /// </summary>
    public int Height => Resolution.Y;

    /// <summary>
    /// Gets the focused state of window.
    /// </summary>
    public bool IsFocused => _core.IsActive;

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
    /// Gets or sets value indicating whether the vertical synchronization is enabled.
    /// </summary>
    public bool UseVSync
    {
        get => _core.GraphicsDeviceManager.SynchronizeWithVerticalRetrace;
        set
        {
            _core.GraphicsDeviceManager.SynchronizeWithVerticalRetrace = value;
            _core.GraphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Disposes the window.
    /// </summary>
    public void Dispose()
    {
    }
}

using System;
using Microsoft.Xna.Framework;

namespace MonoGine;

/// <summary>
/// Represents a window in the game engine.
/// </summary>
public sealed class Window : IObject
{
    private readonly MonoGameBridge _monoGameBridge;
    private readonly IViewport _viewport;

    internal Window(MonoGameBridge monoGameBridge)
    {
        _monoGameBridge = monoGameBridge;
        _monoGameBridge.GraphicsDeviceManager.HardwareModeSwitch = false;
        _viewport = new Viewport(this, monoGameBridge.GraphicsDevice);
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
        get => _monoGameBridge.Window.Title;
        set => _monoGameBridge.Window.Title = value;
    }

    /// <summary>
    /// Gets or sets the position of the window.
    /// </summary>
    public Point Position
    {
        get => _monoGameBridge.Window.Position;
        set => _monoGameBridge.Window.Position = value;
    }

    /// <summary>
    /// Gets or sets the resolution of the window.
    /// </summary>
    public Point Resolution
    {
        get
        {
            GraphicsDeviceManager graphicsDeviceManager = _monoGameBridge.GraphicsDeviceManager;
            return new Point(graphicsDeviceManager.PreferredBackBufferWidth,
                graphicsDeviceManager.PreferredBackBufferHeight);
        }
        set
        {
            if (Resolution == value)
            {
                return;
            }

            _monoGameBridge.GraphicsDeviceManager.PreferredBackBufferWidth = value.X;
            _monoGameBridge.GraphicsDeviceManager.PreferredBackBufferHeight = value.Y;
            _monoGameBridge.GraphicsDeviceManager.ApplyChanges();

            _viewport.Rescale(_monoGameBridge.GraphicsDevice, value);
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
    public bool IsFocused => _monoGameBridge.IsActive;

    /// <summary>
    /// Gets or sets the target framerate of the window.
    /// </summary>
    public int Framerate
    {
        get => (int)Math.Round(1d / _monoGameBridge.TargetElapsedTime.TotalSeconds);
        set => _monoGameBridge.TargetElapsedTime = TimeSpan.FromSeconds(1d / value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is in fullscreen mode.
    /// </summary>
    public bool IsFullscreen
    {
        get => _monoGameBridge.GraphicsDeviceManager.IsFullScreen;
        set
        {
            _monoGameBridge.GraphicsDeviceManager.IsFullScreen = value;
            _monoGameBridge.GraphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    public bool IsBorderless
    {
        get => _monoGameBridge.Window.IsBorderless;
        set => _monoGameBridge.Window.IsBorderless = value;
    }

    /// <summary>
    /// Gets or sets value indicating whether the vertical synchronization is enabled.
    /// </summary>
    public bool UseVSync
    {
        get => _monoGameBridge.GraphicsDeviceManager.SynchronizeWithVerticalRetrace;
        set
        {
            _monoGameBridge.GraphicsDeviceManager.SynchronizeWithVerticalRetrace = value;
            _monoGameBridge.GraphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets value indicating whether the Alt + F4 combination works. 
    /// </summary>
    public bool AllowAltF4
    {
        get => _monoGameBridge.Window.AllowAltF4;
        set => _monoGameBridge.Window.AllowAltF4 = value;
    }

    /// <summary>
    /// Gets or sets value indicating whether the window can be resized.
    /// </summary>
    public bool AllowResizing
    {
        get => _monoGameBridge.Window.AllowUserResizing;
        set => _monoGameBridge.Window.AllowUserResizing = value;
    }

    /// <summary>
    /// Disposes the window.
    /// </summary>
    public void Dispose()
    {
    }
}
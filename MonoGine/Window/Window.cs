using System;
using Microsoft.Xna.Framework;

namespace MonoGine;

/// <summary>
/// Represents a window in the game engine.
/// </summary>
public sealed class Window : IObject
{
    public IViewport Viewport { get; }

    private readonly MonoGameBridge _monoGameBridge;
    private readonly GameWindow _window;
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    internal Window(MonoGameBridge monoGameBridge)
    {
        _monoGameBridge = monoGameBridge;
        _window = monoGameBridge.Window;
        _graphicsDeviceManager = monoGameBridge.GraphicsDeviceManager;
        _graphicsDeviceManager.HardwareModeSwitch = false;
        Viewport = new Viewport(this, monoGameBridge.GraphicsDevice);
        ResolutionChanged?.Invoke(Resolution);
    }

    public event Action<Point>? ResolutionChanged;

    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    public string Title
    {
        get => _window.Title;
        set => _window.Title = value;
    }

    /// <summary>
    /// Gets or sets the position of the window.
    /// </summary>
    public Point Position
    {
        get => _window.Position;
        set => _window.Position = value;
    }

    /// <summary>
    /// Gets or sets the resolution of the window.
    /// </summary>
    public Point Resolution
    {
        get => new(_graphicsDeviceManager.PreferredBackBufferWidth, _graphicsDeviceManager.PreferredBackBufferHeight);
        set
        {
            if (Resolution == value)
            {
                return;
            }

            _graphicsDeviceManager.PreferredBackBufferWidth = value.X;
            _graphicsDeviceManager.PreferredBackBufferHeight = value.Y;
            _graphicsDeviceManager.ApplyChanges();

            ResolutionChanged?.Invoke(value);
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
        get => _graphicsDeviceManager.IsFullScreen;
        set
        {
            _graphicsDeviceManager.IsFullScreen = value;
            _graphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the window is borderless.
    /// </summary>
    public bool IsBorderless
    {
        get => _window.IsBorderless;
        set => _window.IsBorderless = value;
    }

    /// <summary>
    /// Gets or sets value indicating whether the vertical synchronization is enabled.
    /// </summary>
    public bool UseVSync
    {
        get => _graphicsDeviceManager.SynchronizeWithVerticalRetrace;
        set
        {
            _graphicsDeviceManager.SynchronizeWithVerticalRetrace = value;
            _graphicsDeviceManager.ApplyChanges();
        }
    }

    /// <summary>
    /// Gets or sets value indicating whether the Alt + F4 combination works. 
    /// </summary>
    public bool AllowAltF4
    {
        get => _window.AllowAltF4;
        set => _window.AllowAltF4 = value;
    }

    /// <summary>
    /// Gets or sets value indicating whether the window can be resized.
    /// </summary>
    public bool AllowResizing
    {
        get => _window.AllowUserResizing;
        set => _window.AllowUserResizing = value;
    }

    /// <summary>
    /// Disposes the window.
    /// </summary>
    public void Dispose()
    {
        Viewport.Dispose();
    }
}
using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class Window : IObject
{
    private Core _core;

    internal Window(Core core)
    {
        _core = core;
        _core.GraphicsDeviceManager.HardwareModeSwitch = false;
    }

    public string Title
    {
        get => _core.Window.Title;
        set => _core.Window.Title = value;
    }

    public Point Position
    {
        get => _core.Window.Position;
        set => _core.Window.Position = value;
    }

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

    public bool IsFullscreen
    {
        get => _core.GraphicsDeviceManager.IsFullScreen;
        set
        {
            _core.GraphicsDeviceManager.IsFullScreen = value;
            _core.GraphicsDeviceManager.ApplyChanges();
        }
    }

    public bool IsBorderless
    {
        get => _core.Window.IsBorderless;
        set => _core.Window.IsBorderless = value;
    }

    public void Dispose()
    {

    }
}

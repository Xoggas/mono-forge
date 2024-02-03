using System;
using Microsoft.Xna.Framework;

namespace MonoGine.InputSystem;

public sealed class Input : IInput
{
    public event Action<IInputDevice>? AnyDeviceConnected;
    public event Action<IInputDevice>? AnyDeviceDisconnected;
    public event Action<char>? OnTextInput;
    public event Action<string[]>? OnFileDrop;

    private readonly GameWindow _window;

    internal Input(GameWindow window)
    {
        _window = window;
        _window.TextInput += HandleInputFromKeyboard;
        _window.FileDrop += HandleFileDrop;

        foreach (IGamepad gamepad in GamePads)
        {
            gamepad.Connected += () => AnyDeviceConnected?.Invoke(gamepad);
            gamepad.Disconnected += () => AnyDeviceDisconnected?.Invoke(gamepad);
        }
    }

    public IKeyboard Keyboard { get; } = new Keyboard();
    public IMouse Mouse { get; } = new Mouse();

    public IGamepad[] GamePads { get; } =
    {
        new Gamepad(PlayerIndex.One),
        new Gamepad(PlayerIndex.Two),
        new Gamepad(PlayerIndex.Three),
        new Gamepad(PlayerIndex.Four)
    };

    public void Update(IEngine engine)
    {
        Keyboard.Update(engine);
        Mouse.Update(engine);

        for (var i = 0; i < 4; i++)
        {
            GamePads[i].Update(engine);
        }
    }

    public void Dispose()
    {
        _window.TextInput -= HandleInputFromKeyboard;
        _window.FileDrop -= HandleFileDrop;
    }

    private void HandleInputFromKeyboard(object? sender, TextInputEventArgs args)
    {
        OnTextInput?.Invoke(args.Character);
    }

    private void HandleFileDrop(object? sender, FileDropEventArgs args)
    {
        OnFileDrop?.Invoke(args.Files);
    }
}
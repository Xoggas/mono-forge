using System;
using Microsoft.Xna.Framework;

namespace MonoGine.InputSystem;

public sealed class Input : IInput
{
    public event Action<char>? OnTextInput;
    public event Action<string[]>? OnFileDrop;

    private readonly GameWindow _window;

    internal Input(GameWindow window)
    {
        Keyboard = new Keyboard();
        Mouse = new Mouse();
        Gamepads = new IGamepad[]
        {
            new Gamepad(PlayerIndex.One),
            new Gamepad(PlayerIndex.Two),
            new Gamepad(PlayerIndex.Three),
            new Gamepad(PlayerIndex.Four),
        };

        _window = window;
        _window.TextInput += HandleInputFromKeyboard;
        _window.FileDrop += HandleFileDrop;
    }

    public IKeyboard Keyboard { get; }
    public IMouse Mouse { get; }
    public IGamepad[] Gamepads { get; }

    public void Update(IEngine engine)
    {
        Keyboard.Update(engine);
        Mouse.Update(engine);

        for (int i = 0; i < 4; i++)
        {
            Gamepads[i].Update(engine);
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

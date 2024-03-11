using System;
using Microsoft.Xna.Framework.Input;

namespace MonoForge.InputSystem;

public sealed class Keyboard : IKeyboard
{
    private KeyboardState _lastState;
    private KeyboardState _currentState;

    internal Keyboard()
    {
        _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        _lastState = _currentState;
    }

    public event Action? Connected;
    public event Action? Disconnected;
    public bool IsConnected => true;
    public bool CapsLock => _currentState.CapsLock;
    public bool NumLock => _currentState.NumLock;

    public void Update(GameBase gameBase, float deltaTime)
    {
        _lastState = _currentState;
        _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }

    public bool WasPressed(Keys key)
    {
        return _lastState.IsKeyUp(key) && _currentState.IsKeyDown(key);
    }

    public bool IsPressed(Keys key)
    {
        return _currentState.IsKeyDown(key);
    }

    public bool WasReleased(Keys key)
    {
        return _lastState.IsKeyDown(key) && _currentState.IsKeyUp(key);
    }

    public void Dispose()
    {
    }
}
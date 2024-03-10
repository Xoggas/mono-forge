using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoForge.InputSystem;

public sealed class Mouse : IMouse
{
    private MouseState _lastState;
    private MouseState _currentState;

    internal Mouse()
    {
    }

    public event Action? Connected;
    public event Action? Disconnected;
    public bool IsConnected => true;
    public Vector2 Position { get; private set; }
    public Vector2 Delta { get; private set; }
    public float ScrollWheelSpeed { get; private set; }

    public void Update(IGame game, float deltaTime)
    {
        UpdateStates();
        UpdateValues();
    }

    public bool WasPressed(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => WasPressed(_lastState.LeftButton, _currentState.LeftButton),
            MouseButton.Middle => WasPressed(_lastState.MiddleButton, _currentState.MiddleButton),
            MouseButton.Right => WasPressed(_lastState.RightButton, _currentState.RightButton),
            _ => throw new ArgumentException($"Can't get state for {nameof(button)}")
        };
    }

    public bool IsPressed(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => IsPressed(_currentState.LeftButton),
            MouseButton.Middle => IsPressed(_currentState.MiddleButton),
            MouseButton.Right => IsPressed(_currentState.RightButton),
            _ => throw new ArgumentException($"Can't get state for {nameof(button)}")
        };
    }

    public bool WasReleased(MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => WasReleased(_lastState.LeftButton, _currentState.LeftButton),
            MouseButton.Middle => WasReleased(_lastState.MiddleButton, _currentState.MiddleButton),
            MouseButton.Right => WasReleased(_lastState.RightButton, _currentState.RightButton),
            _ => throw new ArgumentException($"Can't get state for {nameof(button)}")
        };
    }

    public void Dispose()
    {
    }

    private void UpdateStates()
    {
        _lastState = _currentState;
        _currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
    }

    private void UpdateValues()
    {
        Position = _currentState.Position.ToVector2();
        Delta = Position - _lastState.Position.ToVector2();
        ScrollWheelSpeed = _currentState.ScrollWheelValue - _lastState.ScrollWheelValue;
    }

    private bool WasPressed(ButtonState last, ButtonState current)
    {
        return last == ButtonState.Released && current == ButtonState.Pressed;
    }

    private bool IsPressed(ButtonState current)
    {
        return current == ButtonState.Pressed;
    }

    private bool WasReleased(ButtonState last, ButtonState current)
    {
        return last == ButtonState.Pressed && current == ButtonState.Released;
    }
}
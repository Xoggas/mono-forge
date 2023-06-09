using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGine.InputSystem;

public sealed class Mouse : InputDevice
{
    private MouseState _previousState;
    private MouseState _currentState;

    public Mouse()
    {
        _previousState = GetState();
        _currentState = _previousState;
    }

    public override bool IsConnected => true;
    public Vector2 Delta => (_currentState.Position - _previousState.Position).ToVector2();
    public Vector2 Position => _currentState.Position.ToVector2();
    public float Wheel => _currentState.ScrollWheelValue;

    public bool WasPressed(MouseButton button)
    {
        var previousState = GetButtonState(_previousState, button);
        var currentState = GetButtonState(_currentState, button);

        return previousState == ButtonState.Released && currentState == ButtonState.Pressed;
    }

    public bool IsPressed(MouseButton button)
    {
        var currentState = GetButtonState(_currentState, button);

        return currentState == ButtonState.Pressed;
    }

    public bool WasReleased(MouseButton button)
    {
        var previousState = GetButtonState(_previousState, button);
        var currentState = GetButtonState(_currentState, button);

        return previousState == ButtonState.Pressed && currentState == ButtonState.Released;
    }

    public override void Dispose()
    {

    }

    internal override void Update()
    {
        _previousState = _currentState;
        _currentState = GetState();
    }

    private MouseState GetState()
    {
        return Microsoft.Xna.Framework.Input.Mouse.GetState();
    }

    private ButtonState GetButtonState(MouseState state, MouseButton button)
    {
        return button switch
        {
            MouseButton.Left => state.LeftButton,
            MouseButton.Middle => state.MiddleButton,
            MouseButton.Right => state.RightButton,
            _ => throw new Exception("Button is not supported!"),
        };
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGine.InputSystem;

public sealed class Gamepad : InputDevice
{
    private PlayerIndex _index;
    private GamePadState _previousState;
    private GamePadState _currentState;

    public Gamepad(PlayerIndex index)
    {
        _index = index;
        _previousState = GetState();
        _currentState = _previousState;
    }

    public override bool IsConnected => _currentState.IsConnected;
    public Vector2 LeftStick => _currentState.ThumbSticks.Left;
    public Vector2 RightStick => _currentState.ThumbSticks.Right;
    public float LeftTrigger => _currentState.Triggers.Left;
    public float RightTrigger => _currentState.Triggers.Right;

    public bool WasPressed(GamepadButton button)
    {
        var nativeEnumValue = ToNativeEnum(button);
        return _previousState.IsButtonUp(nativeEnumValue) && _currentState.IsButtonDown(nativeEnumValue);
    }

    public bool IsPressed(GamepadButton button)
    {
        var nativeEnumValue = ToNativeEnum(button);
        return _currentState.IsButtonDown(nativeEnumValue);
    }

    public bool WasReleased(GamepadButton button)
    {
        var nativeEnumValue = ToNativeEnum(button);
        return _previousState.IsButtonDown(nativeEnumValue) && _currentState.IsButtonUp(nativeEnumValue);
    }

    public override void Dispose()
    {

    }

    internal override void Update()
    {
        _previousState = _currentState;
        _currentState = GetState();
    }

    private GamePadState GetState()
    {
        return GamePad.GetState(_index);
    }

    private Buttons ToNativeEnum(GamepadButton button)
    {
        return (Buttons)button;
    }
}

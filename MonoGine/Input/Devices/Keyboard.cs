using Microsoft.Xna.Framework.Input;

namespace MonoGine.InputSystem;

public sealed class Keyboard : InputDevice
{
    private KeyboardState _previousState;
    private KeyboardState _currentState;

    public Keyboard()
    {
        _previousState = GetState();
        _currentState = _previousState;
    }

    public override bool IsConnected => true;
    public bool CapsLock => _currentState.CapsLock;
    public bool NumLock => _currentState.NumLock;  

    public bool WasPressed(KeyCode keys)
    {
        var nativeEnumValue = ToNativeEnum(keys);
        return _previousState.IsKeyUp(nativeEnumValue) && _currentState.IsKeyDown(nativeEnumValue);
    }

    public bool IsPressed(KeyCode keys)
    {
        var nativeEnumValue = ToNativeEnum(keys);
        return _currentState.IsKeyDown(nativeEnumValue);
    }

    public bool WasReleased(KeyCode keys)
    {
        var nativeEnumValue = ToNativeEnum(keys);
        return _previousState.IsKeyDown(nativeEnumValue) && _currentState.IsKeyUp(nativeEnumValue);
    }

    private Keys ToNativeEnum(KeyCode keys)
    {
        return (Keys)keys;
    }

    public override void Dispose()
    {
        
    }

    internal override void Update()
    {
        _previousState = _currentState;
        _currentState = GetState();
    }

    private KeyboardState GetState()
    {
        return Microsoft.Xna.Framework.Input.Keyboard.GetState();
    }
}

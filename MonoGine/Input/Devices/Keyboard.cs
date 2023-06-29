using Microsoft.Xna.Framework.Input;

namespace MonoGine.InputSystem;

public sealed class Keyboard : IKeyboard
{
    private KeyboardState _lastState;
    private KeyboardState _currentState;

    internal Keyboard()
    {
        _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        _lastState = _currentState;
    }

    public bool IsConnected => true;

    public void Update(IEngine engine)
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

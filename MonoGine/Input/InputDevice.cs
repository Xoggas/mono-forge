namespace MonoGine.InputSystem;

public abstract class InputDevice : Object
{
    public abstract bool IsConnected { get; }   

    internal abstract void Update();
}

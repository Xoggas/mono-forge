namespace MonoGine.InputSystem;

/// <summary>
/// Interface representing an input provider. 
/// </summary>
public interface IInput : IObject, IUpdatable
{
    /// <summary>
    /// Gets the keyboard input device.
    /// </summary>
    public IKeyboard Keyboard { get; }
    
    /// <summary>
    /// Gets the mouse input device.
    /// </summary>
    public IMouse Mouse { get; }
    
    /// <summary>
    /// Gets the array of gamepads.
    /// </summary>
    public IGamepad[] Gamepads { get; }
}

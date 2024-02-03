using System;

namespace MonoGine.InputSystem;

/// <summary>
/// Interface representing an input provider. 
/// </summary>
public interface IInput : IObject, IUpdatable
{
    /// <summary>
    /// An event for handling any device connection.
    /// </summary>
    public event Action<IInputDevice>? AnyDeviceConnected;

    /// <summary>
    /// An event for handling any device disconnection.
    /// </summary>
    public event Action<IInputDevice>? AnyDeviceDisconnected;

    /// <summary>
    /// An event for handling text input from keyboard.
    /// </summary>
    public event Action<char>? OnTextInput;

    /// <summary>
    /// An event for handling drag and drop files. 
    /// </summary>
    public event Action<string[]>? OnFileDrop;

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
    public IGamepad[] GamePads { get; }
}
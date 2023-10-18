using System;

namespace MonoGine.InputSystem;

/// <summary>
/// An interface representing an input device.
/// </summary>
public interface IInputDevice : IObject, IUpdatable
{
    /// <summary>
    /// An event for handling the device connection.
    /// </summary>
    public event Action Connected;

    /// <summary>
    /// An event for handling the device disconnection.
    /// </summary>
    public event Action Disconnected;

    /// <summary>
    /// Gets the input device state.
    /// </summary>
    public bool IsConnected { get; }
}
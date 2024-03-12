using System;

namespace MonoForge.InputSystem;

/// <summary>
/// An interface representing an input device that be connected/disconnected.
/// </summary>
public interface IDisconnectableInputDevice : IInputDevice
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
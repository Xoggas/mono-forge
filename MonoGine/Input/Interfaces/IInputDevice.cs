using System;

namespace MonoGine.InputSystem;

/// <summary>
/// An interface representing an input device.
/// </summary>
public interface IInputDevice : IObject, IUpdatable
{
    /// <summary>
    /// Gets the input device state.
    /// </summary>
    public bool IsConnected { get; }
}

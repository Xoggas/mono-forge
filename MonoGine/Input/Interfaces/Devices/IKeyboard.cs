﻿using Microsoft.Xna.Framework.Input;

namespace MonoGine.InputSystem;

/// <summary>
/// Interface representing a keyboard input device.
/// </summary>
public interface IKeyboard : IInputDevice
{
    /// <summary>
    /// Checks if the specified keyboard key was pressed.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the specified key was pressed, otherwise false.</returns>
    public bool WasPressed(Keys key);

    /// <summary>
    /// Checks if the specified keyboard key is pressed.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the specified key is pressed, otherwise false.</returns>
    public bool IsPressed(Keys key);

    /// <summary>
    /// Checks if the specified keyboard key was released.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>True if the specified key was released, otherwise false.</returns>
    public bool WasReleased(Keys key);
}

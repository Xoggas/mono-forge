using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGine.InputSystem;

/// <summary>
/// Interface representing a gamepad input device.
/// </summary>
public interface IGamepad : IInputDevice
{
    /// <summary>
    /// Gets the left stick value.
    /// </summary>
    public Vector2 LeftStickValue { get; }

    /// <summary>
    /// Gets the right stick value.
    /// </summary>
    public Vector2 RightStickValue { get; }
    
    /// <summary>
    /// Gets the left shoulder value.
    /// </summary>
    public float LeftShoulderValue { get; }
    
    /// <summary>
    /// Gets the right shoulder value.
    /// </summary>
    public float RightShoulderValue { get; }

    /// <summary>
    /// Sets the rumble speed of the gamepad.
    /// </summary>
    /// <param name="left">Strength value for the left motor.</param>
    /// <param name="right">Strength value for the right motor.</param>
    public void SetRumbleSpeed(float left, float right);
    
    /// <summary>
    /// Checks if the specified gamepad button was pressed.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the specified button was pressed, otherwise false.</returns>
    public bool WasPressed(Buttons button);

    /// <summary>
    /// Checks if the specified gamepad button is currently being pressed.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the specified button is pressed, otherwise false.</returns>
    public bool IsPressed(Buttons button);

    /// <summary>
    /// Checks if the specified gamepad button was released.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>True if the specified button was released, otherwise false.</returns>
    public bool WasReleased(Buttons button);
}

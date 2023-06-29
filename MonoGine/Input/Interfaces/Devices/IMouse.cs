using Microsoft.Xna.Framework;

namespace MonoGine.InputSystem;

/// <summary>
/// Interface representing a mouse input device.
/// </summary>
public interface IMouse : IInputDevice
{
    /// <summary>
    /// Gets the current mouse position relatively to the window space.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// Gets the mouse position delta.
    /// </summary>
    public Vector2 Delta { get; }

    /// <summary>
    /// Gets the mouse wheel scrolling speed.
    /// </summary>
    public float ScrollWheelSpeed { get; }

    /// <summary>
    /// Checks if the specified mouse button was pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the specified button was pressed, otherwise false.</returns>
    public bool WasPressed(MouseButton button);

    /// <summary>
    /// Checks if the specified mouse button is pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the specified button is pressed, otherwise false.</returns>
    public bool IsPressed(MouseButton button);

    /// <summary>
    /// Checks if the specified mouse button was released.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>True if the specified button was released, otherwise false.</returns>
    public bool WasReleased(MouseButton button);
}

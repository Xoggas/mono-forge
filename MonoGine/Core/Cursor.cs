using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGine;

/// <summary>
/// Represents a cursor in the game engine.
/// </summary>
public sealed class Cursor : IObject
{
    private readonly Core _core;

    internal Cursor(Core core)
    {
        _core = core;
    }

    /// <summary>
    /// Gets or sets the visibility of the cursor.
    /// </summary>
    public bool IsVisible
    {
        get => _core.IsMouseVisible;
        set => _core.IsMouseVisible = value;
    }

    /// <summary>
    /// Sets the texture of the cursor.
    /// </summary>
    public Texture2D Texture
    {
        set => Mouse.SetCursor(MouseCursor.FromTexture2D(value, 0, 0));
    }

    /// <summary>
    /// Disposes the cursor.
    /// </summary>
    public void Dispose()
    {
        // Dispose implementation goes here
    }
}

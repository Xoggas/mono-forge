using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGine;

public sealed class Cursor : IObject
{
    private Core _core;

    internal Cursor(Core core)
    {
        _core = core;
    }

    public bool IsVisible
    {
        get => _core.IsMouseVisible;
        set => _core.IsMouseVisible = value;
    }

    public Texture2D Texture
    {
        set => Mouse.SetCursor(MouseCursor.FromTexture2D(value, 0, 0));
    }

    public void Dispose()
    {

    }
}

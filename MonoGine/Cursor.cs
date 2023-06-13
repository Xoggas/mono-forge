using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGine;

public static class Cursor
{
    public static bool IsVisible
    {
        get => Core.s_instance.IsMouseVisible;
        set => Core.s_instance.IsMouseVisible = value;
    }

    public static Texture2D Texture
    {
        set => Mouse.SetCursor(MouseCursor.FromTexture2D(value, 0, 0));
    }
}

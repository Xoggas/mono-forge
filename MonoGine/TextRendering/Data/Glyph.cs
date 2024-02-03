using Microsoft.Xna.Framework;

namespace MonoGine.TextRendering;

public readonly struct Glyph
{
    public readonly Vector2 Position;
    public readonly Vector2 Size;

    public Glyph(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }
}
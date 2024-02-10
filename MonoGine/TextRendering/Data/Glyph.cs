using Microsoft.Xna.Framework;

namespace MonoGine.TextRendering;

internal readonly struct Glyph
{
    internal readonly Vector2 _position;
    internal readonly Vector2 _size;

    internal Glyph(Vector2 position, Vector2 size)
    {
        _position = position;
        _size = size;
    }
}
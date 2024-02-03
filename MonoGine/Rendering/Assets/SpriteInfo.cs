using Microsoft.Xna.Framework;

namespace MonoGine.Rendering;

internal sealed class SpriteInfo
{
    internal readonly string Name;
    internal readonly int Id;
    internal readonly Rectangle Bounds;

    internal SpriteInfo(string name, int id, Rectangle bounds)
    {
        Name = name;
        Id = id;
        Bounds = bounds;
    }
}
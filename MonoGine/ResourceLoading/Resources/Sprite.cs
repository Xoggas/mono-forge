using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.ResourceLoading;

public class Sprite : Asset
{
    public Sprite(Metadata metadata) : base(metadata)
    {

    }

    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    public Vector2[] Colliders { get; }
}

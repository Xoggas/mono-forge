using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.ResourceLoading;

public class Sprite : Resource
{
    public Sprite(Metadata metadata, Texture2D texture) : base(metadata)
    {
        Texture = texture;
        Rectangle = metadata["rectangle"].GetValue<Rectangle>();
        Colliders = metadata["collders"].GetValue<Vector2[]>();
    }

    public Texture2D Texture { get; }
    public Rectangle Rectangle { get; }
    public Vector2[] Colliders { get; }
}

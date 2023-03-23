using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.ContentPipeline;

public class Sprite : Resource
{
    public Sprite(Metadata metadata) : base(metadata)
    {

    }

    public Texture2D Texture { get; private set; }

    public override void Dispose()
    {
        Texture = null;
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;
using MonoGine.Rendering.Batching;

namespace MonoGine.SceneGraph.Nodes;

public sealed class SpriteNode : Node
{
    public SpriteNode()
    {
        Color = Color.White;
        TextureRect = new Rectangle(0, 0, 1, 1);
    }

    public Texture2D? Texture { get; set; }
    public Shader? Shader { get; set; }
    public Color Color { get; set; }
    public Rectangle TextureRect { get; set; }

    public override void Draw(IEngine engine, IBatch batch)
    {
        if (Texture != null)
        {
            batch.DrawSprite(Texture, Color, Transform.WorldMatrix, Transform.Pivot, Shader, TextureRect, Transform.WorldDepth);
        }

        base.Draw(engine, batch);
    }
}

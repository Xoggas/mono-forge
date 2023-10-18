using Microsoft.Xna.Framework;
using MonoGine.Rendering;
using MonoGine.Rendering.Batching;

namespace MonoGine.SceneGraph;

public sealed class SpriteNode : Node
{
    public Sprite? Sprite { get; set; }
    public Shader? Shader { get; set; }
    public Color Color { get; set; } = Color.White;
    public Rectangle TextureRect { get; set; } = new(0, 0, 1, 1);

    private readonly Mesh _mesh = Mesh.NewQuad;

    public override void Update(IEngine engine)
    {
        base.Update(engine);
        UpdateMesh();
        UpdateUv();
    }

    public override void Draw(IEngine engine, IBatch batch)
    {
        if (Sprite != null)
        {
            batch.DrawTexturedMesh(Sprite, _mesh, Shader, Transform.WorldDepth);
        }

        base.Draw(engine, batch);
    }

    private void UpdateMesh()
    {
        _mesh.Vertices[0] = new Vertex(Vector3.Transform(new Vector3(-Transform.Pivot, 0f),
            Transform.WorldMatrix), Color);
        _mesh.Vertices[1] = new Vertex(Vector3.Transform(new Vector3(Vector2.UnitY - Transform.Pivot, 0f),
            Transform.WorldMatrix), Color);
        _mesh.Vertices[2] = new Vertex(Vector3.Transform(new Vector3(Vector2.UnitX - Transform.Pivot, 0f),
            Transform.WorldMatrix), Color);
        _mesh.Vertices[3] = new Vertex(Vector3.Transform(new Vector3(Vector2.One - Transform.Pivot, 0f),
            Transform.WorldMatrix), Color);
    }

    private void UpdateUv()
    {
        _mesh.Uvs[0] = new Vector2(TextureRect.X, TextureRect.Y);
        _mesh.Uvs[1] = new Vector2(TextureRect.X, TextureRect.Y + TextureRect.Height);
        _mesh.Uvs[2] = new Vector2(TextureRect.X + TextureRect.Width, TextureRect.Y);
        _mesh.Uvs[3] = new Vector2(TextureRect.X + TextureRect.Width, TextureRect.Y + TextureRect.Height);
    }
}
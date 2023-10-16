using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;
using MonoGine.Rendering.Batching;

namespace MonoGine.SceneGraph;

public sealed class SpriteNode : Node
{
    private readonly Mesh _mesh = Mesh.NewQuad;

    public Texture2D? Texture { get; set; }
    public Shader? Shader { get; set; }
    public Color Color { get; set; } = Color.White;

    public override void Update(IEngine engine)
    {
        base.Update(engine);
        UpdateMesh();
    }

    public override void Draw(IEngine engine, IBatch batch)
    {
        if (Texture != null)
        {
            batch.DrawTexturedMesh(Texture, _mesh, Shader, Transform.WorldDepth);
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
}
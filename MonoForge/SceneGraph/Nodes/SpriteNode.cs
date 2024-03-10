using System;
using Microsoft.Xna.Framework;
using MonoForge.Rendering;
using MonoForge.Rendering.Batching;

namespace MonoForge.SceneGraph;

public sealed class SpriteNode : Node
{
    public Sprite? Sprite { get; set; }
    public Shader? Shader { get; set; }
    public Color Color { get; set; } = Color.White;
    public Rectangle TextureRect { get; set; } = new(0, 0, 1, 1);

    private readonly Mesh _mesh = Mesh.NewQuad;

    public override void Update(IGame game, float deltaTime)
    {
        base.Update(game, deltaTime);
        UpdateMesh();
        UpdateUv();
    }

    public override Action<float> GetPropertySetter(string name)
    {
        return name switch
        {
            "color.r" => value => Color = new Color(value, Color.G, Color.B, Color.A),
            "color.g" => value => Color = new Color(Color.R, value, Color.B, Color.A),
            "color.b" => value => Color = new Color(Color.R, Color.G, value, Color.A),
            "color.a" => value => Color = new Color(Color.R, Color.G, Color.B, value),
            _ => name.StartsWith('_') ? value => Shader?.Properties.Set(name[1..], value) : base.GetPropertySetter(name)
        };
    }

    public override void Draw(IGame game, IRenderQueue renderQueue)
    {
        if (Sprite != null)
        {
            renderQueue.EnqueueTexturedMesh(Sprite, _mesh, Shader, Transform.WorldDepth);
        }

        base.Draw(game, renderQueue);
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
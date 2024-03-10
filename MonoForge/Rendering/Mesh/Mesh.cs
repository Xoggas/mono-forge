using System;
using Microsoft.Xna.Framework;

namespace MonoForge.Rendering;

public sealed class Mesh
{
    public static Mesh NewQuad => new()
    {
        Vertices = new[]
        {
            new Vertex(new Vector3(0f, 0f, 0f), Color.White),
            new Vertex(new Vector3(0f, 1f, 0f), Color.White),
            new Vertex(new Vector3(1f, 0f, 0f), Color.White),
            new Vertex(new Vector3(1f, 1f, 0f), Color.White)
        },
        Indices = new[]
        {
            0, 2, 1,
            2, 3, 1
        },
        Uvs = new[]
        {
            new Vector2(0f, 0f),
            new Vector2(0f, 1f),
            new Vector2(1f, 0f),
            new Vector2(1f, 1f)
        }
    };

    public Vertex[] Vertices = Array.Empty<Vertex>();
    public int[] Indices = Array.Empty<int>();
    public Vector2[] Uvs = Array.Empty<Vector2>();
}
using System;
using Microsoft.Xna.Framework;

namespace MonoGine.Rendering;

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
        Indices = new short[]
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
    public short[] Indices = Array.Empty<short>();
    public Vector2[] Uvs = Array.Empty<Vector2>();
}
using Microsoft.Xna.Framework;

namespace MonoForge.Rendering;

public struct Vertex
{
    public Vector3 Position;
    public Color Color;

    public Vertex(Vector3 position, Color color)
    {
        Position = position;
        Color = color;
    }
}
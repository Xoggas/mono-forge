using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering.Batching;

public struct BatchPassResult
{
    public Texture2D Texture;
    public Shader? Shader;
    public VertexPositionColorTexture[] Vertices;
    public int VertexCount;
    public int[] Indices;
    public int PrimitiveCount;
}
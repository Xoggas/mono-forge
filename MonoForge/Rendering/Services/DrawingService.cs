using Microsoft.Xna.Framework.Graphics;
using MonoForge.Rendering.Batching;

namespace MonoForge.Rendering;

internal sealed class DrawingService : IDrawingService
{
    public void DrawMeshes(GraphicsDevice graphicsDevice, BatchPassResult pass)
    {
        Shader? shader = pass.Shader;

        if (shader is not null)
        {
            shader.ApplyProperties();

            foreach (EffectPass? effectPass in shader.Passes)
            {
                effectPass.Apply();

                DrawSeparatePass(graphicsDevice, pass);
            }
        }
        else
        {
            DrawSeparatePass(graphicsDevice, pass);
        }
    }

    private static void DrawSeparatePass(GraphicsDevice graphicsDevice, BatchPassResult pass)
    {
        graphicsDevice.Textures[0] = pass.Texture;
        graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, pass.Vertices, 0, pass.VertexCount,
            pass.Indices, 0, pass.PrimitiveCount, VertexPositionColorTexture.VertexDeclaration);
    }
}
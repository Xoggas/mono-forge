using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering.Batching;

namespace MonoGine.Rendering;

internal static class DrawUtility
{
    internal static void Draw(GraphicsDevice graphicsDevice, BatchPassResult batchPassResult)
    {
        Shader? shader = batchPassResult.Shader;

        if (shader is not null)
        {
            shader.ApplyProperties();

            foreach (EffectPass? effectPass in shader.Passes)
            {
                effectPass.Apply();

                DrawSeparatePass(graphicsDevice, batchPassResult);
            }
        }
        else
        {
            DrawSeparatePass(graphicsDevice, batchPassResult);
        }
    }

    private static void DrawSeparatePass(GraphicsDevice graphicsDevice, BatchPassResult pass)
    {
        graphicsDevice.Textures[0] = pass.Texture;
        graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, pass.Vertices, 0, pass.VertexCount,
            pass.Indices, 0, pass.PrimitiveCount, VertexPositionColorTexture.VertexDeclaration);
    }
}
using Microsoft.Xna.Framework.Graphics;
using MonoForge.Rendering.Batching;

namespace MonoForge.Rendering;

public interface IDrawingService
{
    public void DrawMeshes(GraphicsDevice graphicsDevice, BatchPassResult pass);
}
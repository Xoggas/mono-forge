using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering.Batching;

namespace MonoGine.Rendering;

public interface IDrawingService
{
    public void DrawMeshes(GraphicsDevice graphicsDevice, BatchPassResult pass);
}
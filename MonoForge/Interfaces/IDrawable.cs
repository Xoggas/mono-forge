using MonoForge.Rendering.Batching;

namespace MonoForge;

/// <summary>
/// Represents an interface for drawable objects.
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Draws the object using the specified engine and batcher.
    /// </summary>
    /// <param name="game">The game engine.</param>
    /// <param name="renderQueue">The batcher used for rendering.</param>
    public void Draw(IGame game, IRenderQueue renderQueue);
}

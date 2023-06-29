using MonoGine.Rendering.Batching;

namespace MonoGine;

/// <summary>
/// Represents an interface for drawable objects.
/// </summary>
public interface IDrawable
{
    /// <summary>
    /// Draws the object using the specified engine and batcher.
    /// </summary>
    /// <param name="engine">The game engine.</param>
    /// <param name="batch">The batcher used for rendering.</param>
    public void Draw(IEngine engine, IBatch batch);
}

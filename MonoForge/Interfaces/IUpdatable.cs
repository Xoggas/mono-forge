namespace MonoForge;

/// <summary>
/// Represents an interface for objects that can be updated.
/// </summary>
public interface IUpdatable
{
    /// <summary>
    /// Updates the object using the specified engine.
    /// </summary>
    /// <param name="game">The engine used for the update.</param>
    /// <param name="deltaTime"></param>
    public void Update(IGame game, float deltaTime);
}
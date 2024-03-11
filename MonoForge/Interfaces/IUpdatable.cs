namespace MonoForge;

/// <summary>
/// Represents an interface for objects that can be updated.
/// </summary>
public interface IUpdatable
{
    /// <summary>
    /// Updates the object using the specified engine.
    /// </summary>
    /// <param name="gameBase">The engine used for the update.</param>
    /// <param name="deltaTime"></param>
    public void Update(GameBase gameBase, float deltaTime);
}
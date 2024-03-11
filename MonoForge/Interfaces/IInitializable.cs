namespace MonoForge;

/// <summary>
/// Represents an interface for objects that require initialization.
/// </summary>
public interface IInitializable
{
    /// <summary>
    /// Initializes the object with the specified game engine.
    /// </summary>
    /// <param name="gameBase">The game engine.</param>
    public void Initialize(GameBase gameBase);
}
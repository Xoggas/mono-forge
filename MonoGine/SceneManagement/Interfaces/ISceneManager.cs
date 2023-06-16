namespace MonoGine.SceneManagement;

/// <summary>
/// Represents an interface for managing scenes in the game.
/// </summary>
public interface ISceneManager : IUpdatable, IDrawable, IObject
{
    /// <summary>
    /// Gets the current active scene.
    /// </summary>
    public IScene? CurrentScene { get; }

    /// <summary>
    /// Loads a new scene of type T and sets it as the current scene.
    /// </summary>
    /// <typeparam name="T">The type of scene to load.</typeparam>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="loadArgs">Optional arguments passed during scene loading.</param>
    /// <param name="unloadArgs">Optional arguments passed during scene unloading.</param>
    /// <returns>The loaded scene.</returns>
    public IScene Load<T>(Engine engine, object[]? loadArgs = null, object[]? unloadArgs = null) where T : IScene;
}

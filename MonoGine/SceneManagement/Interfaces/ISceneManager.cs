namespace MonoGine.SceneManagement;

/// <summary>
/// Represents an interface for managing scenes in the game.
/// </summary>
public interface ISceneManager : IUpdatable, IObject
{
    /// <summary>
    /// Gets the current active scene.
    /// </summary>
    public IScene? CurrentScene { get; }

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="scene">The scene that will be loaded.</param>
    /// <param name="loadArgs">Optional arguments passed during scene loading.</param>
    /// <param name="unloadArgs">Optional arguments passed during scene unloading.</param>
    public void Load(IEngine engine, IScene scene, object[]? loadArgs = null, object[]? unloadArgs = null);
}

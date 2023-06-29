namespace MonoGine.SceneManagement;

/// <summary>
/// A class that manages scenes in the game.
/// </summary>
public sealed class SceneManager : ISceneManager
{
    /// <summary>
    /// Gets the current active scene.
    /// </summary>
    public IScene? CurrentScene { get; private set; }

    /// <summary>
    /// Loads a scene.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="scene">The scene that will be loaded.</param>
    /// <param name="loadArgs">Optional arguments passed during scene loading.</param>
    /// <param name="unloadArgs">Optional arguments passed during scene unloading.</param>
    public void Load(IEngine engine, IScene scene, object[]? loadArgs = null, object[]? unloadArgs = null)
    {
        CurrentScene?.Unload(engine, unloadArgs);
        CurrentScene = scene;
        CurrentScene.Load(engine, loadArgs);
    }

    /// <summary>
    /// Updates the current scene.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    public void Update(IEngine engine)
    {
        CurrentScene?.Update(engine);
    }

    /// <summary>
    /// Disposes the current scene and its associated resources.
    /// </summary>
    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}

using MonoGine.Graphics;
using System;

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
    /// Loads a new scene of type T and sets it as the current scene.
    /// </summary>
    /// <typeparam name="T">The type of scene to load.</typeparam>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="loadArgs">Optional arguments passed during scene loading.</param>
    /// <param name="unloadArgs">Optional arguments passed during scene unloading.</param>
    /// <returns>The loaded scene.</returns>
    public IScene Load<T>(Engine engine, object[]? loadArgs = null, object[]? unloadArgs = null) where T : IScene
    {
        CurrentScene?.Unload(engine, unloadArgs);
        CurrentScene = Activator.CreateInstance<T>();
        CurrentScene.Load(engine, loadArgs);

        return CurrentScene;
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
    /// Draws the current scene.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="batcher">The batcher used for rendering.</param>
    public void Draw(IEngine engine, IBatcher batcher)
    {
        CurrentScene?.Draw(engine, batcher);
    }

    /// <summary>
    /// Disposes the current scene and its associated resources.
    /// </summary>
    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}
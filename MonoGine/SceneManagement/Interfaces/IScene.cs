using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.UI;
using World = Box2DX.Dynamics.World;

namespace MonoGine.SceneManagement;

/// <summary>
/// Represents an interface for a scene in the game.
/// </summary>
public interface IScene : IUpdatable, IDrawable, IObject
{
    /// <summary>
    /// Gets the world associated with the scene.
    /// </summary>
    public IWorld World { get; }

    /// <summary>
    /// Gets the root of the scene hierarchy tree.
    /// </summary>
    public Node Root { get; }

    /// <summary>
    /// Gets the physics module.
    /// </summary>
    public World Physics { get; }

    /// <summary>
    /// Gets the camera associated with the world.
    /// </summary>
    public ICamera Camera { get; }

    /// <summary>
    /// Gets the canvas associated with the scene.
    /// </summary>
    public ICanvas Canvas { get; }

    /// <summary>
    /// Loads the scene with the specified engine and optional arguments.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene loading.</param>
    internal void Load(IEngine engine, object[]? args);

    /// <summary>
    /// Unloads the scene with the specified engine and optional arguments.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene unloading.</param>
    internal void Unload(IEngine engine, object[]? args);
}
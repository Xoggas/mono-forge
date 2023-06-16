using MonoGine.Ecs;
using MonoGine.Graphics;
using MonoGine.UI;
using PhysicsWorld = Genbox.VelcroPhysics.Dynamics.World;

namespace MonoGine.SceneManagement;

/// <summary>
/// Represents an interface for a scene in the game.
/// </summary>
public interface IScene : IUpdatable, IDrawable, IObject
{
    /// <summary>
    /// Gets the world associated with the scene.
    /// </summary>
    public IWorld? World { get; }

    /// <summary>
    /// Gets the physics world associated with the scene.
    /// </summary>
    public PhysicsWorld? Physics { get; }

    /// <summary>
    /// Gets the camera associated with the scene.
    /// </summary>
    public Camera? Camera { get; }

    /// <summary>
    /// Gets the canvas associated with the scene.
    /// </summary>
    public Canvas? Canvas { get; }

    /// <summary>
    /// Loads the scene with the specified engine and optional arguments.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene loading.</param>
    internal void Load(Engine engine, object[]? args);

    /// <summary>
    /// Unloads the scene with the specified engine and optional arguments.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene unloading.</param>
    internal void Unload(Engine engine, object[]? args);
}

using Microsoft.Xna.Framework;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.UI;
using Physics = Genbox.VelcroPhysics.Dynamics.World;

namespace MonoGine.SceneManagement;

/// <summary>
/// Represents a scene in the game that manages entities, physics, camera, and UI canvas.
/// </summary>
public abstract class Scene : IScene
{
    /// <summary>
    /// Initializes a new instance of the Scene class.
    /// </summary>
    protected Scene()
    {
        World = new World();
        Root = new Node();
        Physics = new Physics(Vector2.Zero);
        Camera = new Camera();
        Canvas = new Canvas();
    }

    /// <summary>
    /// Gets the world associated with the scene.
    /// </summary>
    public IWorld World { get; }

    /// <summary>
    /// Gets the root of the scene hierarchy tree.
    /// </summary>
    public Node Root { get; }

    /// <summary>
    /// Gets the physics world associated with the scene.
    /// </summary>
    public Physics Physics { get; }

    /// <summary>
    /// Gets the main camera.
    /// </summary>
    public ICamera Camera { get; }

    /// <summary>
    /// Gets the canvas associated with the scene.
    /// </summary>
    public ICanvas Canvas { get; }

    /// <summary>
    /// Updates the scene.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    public virtual void Update(IEngine engine)
    {
        Physics.Step(engine.Time.DeltaTime);
        Camera.Update(engine);
        World.Update(engine);
        Root.Update(engine);
        Canvas.Update(engine);
    }

    /// <summary>
    /// Disposes the scene and its associated resources.
    /// </summary>
    public virtual void Dispose()
    {
        World.Dispose();
        Physics.Clear();
        Camera.Dispose();
        Root.Dispose();
        Canvas.Dispose();
    }

    /// <summary>
    /// Called when the scene is loaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene loading.</param>
    protected abstract void OnLoad(IEngine engine, object[]? args);

    /// <summary>
    /// Called when the scene is unloaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene unloading.</param>
    protected abstract void OnUnload(IEngine engine, object[]? args);

    /// <summary>
    /// Called when the scene's resources are loaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    protected abstract void OnLoadResources(IEngine engine);

    void IScene.Load(IEngine engine, object[]? args)
    {
        OnLoadResources(engine);
        OnLoad(engine, args);
    }

    void IScene.Unload(IEngine engine, object[]? args)
    {
        OnUnload(engine, args);
        Dispose();
    }
}

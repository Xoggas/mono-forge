using Box2DX.Collision;
using Box2DX.Common;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.UI;

namespace MonoGine.SceneManagement;

public abstract class Scene : IScene
{
    public Node Root { get; } = new();
    public ICamera Camera { get; } = new Camera();
    public ICanvas Canvas { get; } = new Canvas();
    public IWorld World { get; } = new World();

    public Box2DX.Dynamics.World Physics { get; } =
        new(new AABB { LowerBound = Vec2.Zero, UpperBound = Vec2.Zero }, Vec2.Zero, true);

    public virtual void Update(IEngine engine)
    {
        Camera.Update(engine);
        Physics.Step(engine.Time.DeltaTime, 1, 1);
        World.Update(engine);
        Root.Update(engine);
        Canvas.Update(engine);
    }

    public virtual void Dispose()
    {
        World.Dispose();
        Camera.Dispose();
        Root.Dispose();
        Canvas.Dispose();
    }

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
}
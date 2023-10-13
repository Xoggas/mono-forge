using Box2DX.Collision;
using Box2DX.Common;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.UI;
using World = Box2DX.Dynamics.World;

namespace MonoGine.SceneManagement;

public abstract class Scene : IScene
{
    protected Scene()
    {
        World = new Ecs.World();
        Physics = new World(new AABB {LowerBound = Vec2.Zero, UpperBound = Vec2.Zero}, Vec2.Zero, true);
        Root = new Node();
        Camera = new Camera();
        Canvas = new Canvas();
    }

    public IWorld World { get; }
    public World Physics { get; }
    public Node Root { get; }
    public ICamera Camera { get; }
    public ICanvas Canvas { get; }

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
    ///     Called when the scene is loaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene loading.</param>
    protected abstract void OnLoad(IEngine engine, object[]? args);

    /// <summary>
    ///     Called when the scene is unloaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="args">Optional arguments passed during scene unloading.</param>
    protected abstract void OnUnload(IEngine engine, object[]? args);

    /// <summary>
    ///     Called when the scene's resources are loaded.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    protected abstract void OnLoadResources(IEngine engine);
}
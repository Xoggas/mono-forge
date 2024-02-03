using Box2DX.Collision;
using Box2DX.Common;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.Rendering.Batching;
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
        new(new AABB { LowerBound = Vec2.Zero, UpperBound = Vec2.Zero }, new Vec2(0f, 9.8f), true);

    public virtual void Draw(IEngine engine, IRenderQueue renderQueue)
    {
        Root.Draw(engine, renderQueue);
        Canvas.Draw(engine, renderQueue);
    }

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

    protected abstract void OnLoad(IEngine engine, object[]? args);
    protected abstract void OnUnload(IEngine engine, object[]? args);
    protected abstract void OnLoadResources(IEngine engine);
}
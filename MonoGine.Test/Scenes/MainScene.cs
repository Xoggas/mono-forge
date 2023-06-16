using Microsoft.Xna.Framework.Input;
using MonoGine.Ecs;
using MonoGine.Graphics;
using MonoGine.SceneManagement;
using System.Diagnostics;

namespace MonoGine.Test.Scenes;

public sealed class MainScene : Scene
{
    private IComponent? _component;

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {
            _component?.Entity?.Destroy();
        }
    }

    protected override void OnLoad(Engine engine, object[]? args)
    {
        _component = World.CreateEntity<Dummy>().AddComponent<DummyComponent>();
    }

    protected override void OnLoadResources(Engine engine)
    {

    }

    protected override void OnUnload(Engine engine, object[]? args)
    {

    }
}

public sealed class Dummy : Entity
{
    public override void Start(IEngine engine)
    {
        base.Start(engine);

        Debug.Print("Entity start!");
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        Debug.Print("Entity update!");
    }

    public override void Draw(IEngine engine, IBatcher batcher)
    {
        base.Draw(engine, batcher);

        Debug.Print("Entity draw!");
    }

    public override void Destroy()
    {
        base.Destroy();

        Debug.Print("Entity destroy!");
    }

    public override void Dispose()
    {
        base.Dispose();

        Debug.Print("Entity dispose!");
    }
}

public sealed class DummyComponent : Component
{
    public DummyComponent(IEntity entity) : base(entity)
    {

    }

    public override void Start(IEngine engine)
    {
        base.Start(engine);

        Debug.Print("Component start!");
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        Debug.Print("Component update!");
    }

    public override void Draw(IEngine engine, IBatcher batcher)
    {
        base.Draw(engine, batcher);

        Debug.Print("Component draw!");
    }

    public override void Destroy()
    {
        base.Destroy();

        Debug.Print("Component destroy!");
    }

    public override void Dispose()
    {
        base.Dispose();

        Debug.Print("Component dispose!");
    }
}

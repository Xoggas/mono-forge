using MonoForge.Ecs;
using MonoForge.Rendering;
using MonoForge.Rendering.Batching;
using MonoForge.SceneGraph;
using MonoForge.UI;

namespace MonoForge.SceneManagement;

public abstract class Scene
{
    public Node Root { get; } = new();
    public Camera Camera { get; } = new();
    public Canvas Canvas { get; } = new();
    public World World { get; } = new();

    public virtual void Draw(GameBase gameBase, IRenderQueue renderQueue)
    {
        Root.Draw(gameBase, renderQueue);
        Canvas.Draw(gameBase, renderQueue);
    }

    public virtual void Update(GameBase gameBase)
    {
        Camera.Update(gameBase.Window.Viewport.Resolution);
        World.Update(gameBase, gameBase.Time.DeltaTime);
        Root.Update(gameBase, gameBase.Time.DeltaTime);
        Canvas.Update(gameBase, gameBase.Time.DeltaTime);
    }

    public virtual void Dispose()
    {
        World.Dispose();
        Canvas.Dispose();
    }

    internal void Load(GameBase gameBase, object[]? args)
    {
        OnLoadResources(gameBase);
        OnLoad(gameBase, args);
    }

    internal void Unload(GameBase gameBase, object[]? args)
    {
        OnUnload(gameBase, args);
        Dispose();
    }

    protected abstract void OnLoad(GameBase gameBase, object[]? args);
    protected abstract void OnUnload(GameBase gameBase, object[]? args);
    protected abstract void OnLoadResources(GameBase gameBase);
}
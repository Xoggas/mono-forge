using MonoForge.Ecs;
using MonoForge.Rendering;
using MonoForge.Rendering.Batching;
using MonoForge.SceneGraph;
using MonoForge.SceneManagement.Interfaces;
using MonoForge.UI;

namespace MonoForge.SceneManagement;

public abstract class Scene
{
    public Node Root { get; } = new();
    public Camera Camera { get; } = new();
    public Canvas Canvas { get; } = new();
    public World World { get; } = new();

    protected Scene(GameBase gameBase, ISceneLoadingArgs args)
    {
    }

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

    internal void Unload(GameBase gameBase)
    {
        OnUnload(gameBase);
        Dispose();
    }

    protected abstract void OnUnload(GameBase gameBase);
}
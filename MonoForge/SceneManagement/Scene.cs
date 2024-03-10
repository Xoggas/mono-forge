using Microsoft.Xna.Framework;
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

    public virtual void Draw(IGame game, IRenderQueue renderQueue)
    {
        Root.Draw(game, renderQueue);
        Canvas.Draw(game, renderQueue);
    }

    public virtual void Update(IGame game)
    {
        Camera.Update(new Point(), new Point()); //TODO: Fix
        World.Update(game, game.Time.DeltaTime);
        Root.Update(game, game.Time.DeltaTime);
        Canvas.Update(game, game.Time.DeltaTime);
    }

    public virtual void Dispose()
    {
        World.Dispose();
        Root.Dispose();
        Canvas.Dispose();
    }

    internal void Load(IGame game, object[]? args)
    {
        OnLoadResources(game);
        OnLoad(game, args);
    }

    internal void Unload(IGame game, object[]? args)
    {
        OnUnload(game, args);
        Dispose();
    }

    protected abstract void OnLoad(IGame game, object[]? args);
    protected abstract void OnUnload(IGame game, object[]? args);
    protected abstract void OnLoadResources(IGame game);
}
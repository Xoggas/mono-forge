using System;
using MonoForge.SceneManagement.Interfaces;

namespace MonoForge.SceneManagement;

public sealed class SceneManager
{
    public Scene? CurrentScene { get; private set; }

    public void Load<T>(GameBase gameBase, ISceneLoadingArgs args) where T : Scene
    {
        CurrentScene?.Unload(gameBase);
        CurrentScene = Activator.CreateInstance(typeof(T), gameBase, args) as T;
    }

    public void Update(GameBase gameBase)
    {
        CurrentScene?.Update(gameBase);
    }

    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}
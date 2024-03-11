namespace MonoForge.SceneManagement;

public sealed class SceneManager
{
    public Scene? CurrentScene { get; private set; }

    public void Load(GameBase gameBase, Scene scene, object[]? loadArgs = null, object[]? unloadArgs = null)
    {
        CurrentScene?.Unload(gameBase, unloadArgs);
        CurrentScene = scene;
        CurrentScene.Load(gameBase, loadArgs);
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
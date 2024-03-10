namespace MonoForge.SceneManagement;

public sealed class SceneManager
{
    public Scene? CurrentScene { get; private set; }

    public void Load(IGame game, Scene scene, object[]? loadArgs = null, object[]? unloadArgs = null)
    {
        CurrentScene?.Unload(game, unloadArgs);
        CurrentScene = scene;
        CurrentScene.Load(game, loadArgs);
    }

    public void Update(IGame game)
    {
        CurrentScene?.Update(game);
    }

    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}
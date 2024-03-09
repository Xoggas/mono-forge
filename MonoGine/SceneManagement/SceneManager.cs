namespace MonoGine.SceneManagement;

public sealed class SceneManager
{
    public Scene? CurrentScene { get; private set; }

    public void Load(IEngine engine, Scene scene, object[]? loadArgs = null, object[]? unloadArgs = null)
    {
        CurrentScene?.Unload(engine, unloadArgs);
        CurrentScene = scene;
        CurrentScene.Load(engine, loadArgs);
    }

    public void Update(IEngine engine)
    {
        CurrentScene?.Update(engine);
    }

    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}
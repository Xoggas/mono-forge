namespace MonoGine.SceneManagement;

public sealed class SceneManager : ISceneManager
{
    public IScene? CurrentScene { get; private set; }

    public void Load<T>(object[] args) where T : Scene
    {
        
    }

    public void Unload()
    {
        
    }

    public void Update(Engine engine)
    {
        
    }

    public void Dispose()
    {
        CurrentScene?.Dispose();
    }
}

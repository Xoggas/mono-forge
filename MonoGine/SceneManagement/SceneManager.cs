namespace MonoGine.SceneManagement;

public sealed class SceneManager : System
{
    private Scene _current;

    public void Load<T>(params object[] objects) where T : Scene
    {

    }

    public void Unload()
    {

    }

    public override void Initialize()
    {
        
    }

    public override void PreUpdate()
    {
        _current.PreUpdate();
    }

    public override void PostUpdate()
    {
        _current.PostUpdate();
    }

    public override void Dispose()
    {
        _current.Dispose();
    }
}

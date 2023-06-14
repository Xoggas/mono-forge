namespace MonoGine.SceneManagement;

public interface ISceneManager : IUpdatable, IObject
{
    public IScene? CurrentScene { get; }
    public void Load<T>(object[] args) where T : Scene;
    public void Unload();
}

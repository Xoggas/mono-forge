using MonoGine.Audio;
using MonoGine.Ecs;
using MonoGine.UI;

namespace MonoGine.SceneManagement;

public abstract class Scene : Object
{
    private World _world;
    private AudioManager _audioManager;
    private Canvas _canvas;

    public Scene()
    {
        _world = new World();
        _canvas = new Canvas();
        _audioManager = new AudioManager();
    }

    public abstract void Load(params string[] args);

    public abstract void Update();

    public virtual void Unload()
    {
        _world.Dispose();
        _canvas.Dispose();
        _audioManager.Dispose();
    }
}

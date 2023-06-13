using Microsoft.Xna.Framework;
using MonoGine.Audio;
using MonoGine.Interfaces;

namespace MonoGine.SceneManagement;

public abstract class Scene : Object
{
    private Ecs.World _world;
    private Genbox.VelcroPhysics.Dynamics.World _physics;
    private AudioManager _audioManager;
    private Canvas _canvas;

    public Scene()
    {
        _world = new Ecs.World();
        _physics = new Genbox.VelcroPhysics.Dynamics.World(Vector2.Zero);
        _canvas = new Canvas();
        _audioManager = new AudioManager();
    }

    public Ecs.World World => _world;
    public Genbox.VelcroPhysics.Dynamics.World Physics => _physics;
    public AudioManager AudioManager => _audioManager;
    public Canvas Canvas => _canvas;

    public virtual void Load(params object[] args)
    {

    }

    public virtual void Unload()
    {
        _world.Dispose();
        _physics.Clear();
        _canvas.Dispose();
        _audioManager.Dispose();
    }

    public virtual void PreUpdate()
    {
        _physics.Step(Time.DeltaTime);
        _audioManager.Update();
    }

    public virtual void PostUpdate()
    {
        _world.Update();
        _canvas.Update();
    }
}

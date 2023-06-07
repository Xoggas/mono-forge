using Genbox.VelcroPhysics.Dynamics;
using Microsoft.Xna.Framework;
using MonoGine.Audio;
using MonoGine.Ecs;
using MonoGine.UI;

namespace MonoGine.SceneManagement;

public abstract class Scene : Object
{
    private ECSWorld _ecsWorld;
    private World _world;
    private AudioManager _audioManager;
    private Canvas _canvas;

    public Scene()
    {
        _ecsWorld = new ECSWorld();
        _world = new World(Vector2.Zero);
        _canvas = new Canvas();
        _audioManager = new AudioManager();
    }

    public virtual void Load(params string[] args)
    {

    }

    public virtual void PreUpdate()
    {
        _world.Step(Time.DeltaTime);
    }

    public virtual void PostUpdate()
    {
        _audioManager.Update();
        _ecsWorld.Update();
        _canvas.Update();
    }

    public virtual void Unload()
    {
        _ecsWorld.Dispose();
        _world.Clear();
        _canvas.Dispose();
        _audioManager.Dispose();
    }
}

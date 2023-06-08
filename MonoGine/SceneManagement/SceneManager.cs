using System;

namespace MonoGine.SceneManagement;

public sealed class SceneManager : System
{
    private Scene _current;

    public void Load<T>(params object[] objects) where T : Scene
    {
        _current?.Unload();
        _current = Activator.CreateInstance(typeof(T), objects) as T;
        _current?.Load(objects);
    }

    public override void Initialize()
    {
        
    }

    public override void PreUpdate()
    {
        _current?.PreUpdate();
    }

    public override void PostUpdate()
    {
        _current?.PostUpdate();
    }

    public override void Dispose()
    {
        _current?.Dispose();
    }
}

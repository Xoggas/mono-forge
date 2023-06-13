using System;

namespace MonoGine.SceneManagement;

public sealed class SceneManager : System
{
    private static Scene _current;

    public static void Load<T>(params object[] objects) where T : Scene
    {
        _current?.Unload();
        _current = Activator.CreateInstance(typeof(T), objects) as T;
        _current?.Load(objects);
    }

    public static void Unload()
    {
        _current?.Unload();
    }

    public override void Dispose()
    {
        _current?.Dispose();
    }

    internal override void Initialize()
    {

    }

    internal override void PreUpdate()
    {
        _current?.PreUpdate();
    }

    internal override void PostUpdate()
    {
        _current?.PostUpdate();
    }
}

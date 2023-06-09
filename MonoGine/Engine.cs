using System;

namespace MonoGine;

public abstract class Engine : IDisposable
{
    private static Engine s_instance;

    private Core _core;

    public Engine()
    {
        s_instance = this;

        _core = new Core();
        _core.OnStart += OnStart;
        _core.OnUpdate += OnUpdate;
        _core.OnQuit += OnQuit;
    }

    public static void Quit()
    {
        s_instance._core.Quit();
    }

    public void Run()
    {
        _core.Run();
    }

    public void Dispose()
    {
        _core.Dispose();
    }

    protected abstract void OnStart();

    protected abstract void OnUpdate();

    protected abstract void OnQuit();
}
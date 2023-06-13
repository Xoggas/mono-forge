using System;

namespace MonoGine;

public abstract class Engine : IDisposable
{
    private static Core s_core;

    public Engine()
    {
        s_core = new Core();
        s_core.OnPreInitialize += OnPreInitialize;
        s_core.OnPostInitialize += OnPostInitialize;
        s_core.OnUpdate += OnUpdate;
        s_core.OnQuit += OnQuit;
    }

    public static void Quit()
    {
        s_core.Quit();
    }

    public void Run()
    {
        s_core.Run();
    }

    public void Dispose()
    {
        s_core.Dispose();
    }

    protected abstract void OnPreInitialize();
    protected abstract void OnPostInitialize();
    protected abstract void OnUpdate();
    protected abstract void OnQuit();
}
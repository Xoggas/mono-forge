using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.InputSystem;
using MonoGine.Rendering;
using MonoGine.ResourceLoading;
using MonoGine.SceneManagement;
using System;

namespace MonoGine;

internal sealed class Core : Game
{
    public event Action OnPreInitialize;
    public event Action OnPostInitialize;
    public event Action OnUpdate;
    public event Action OnQuit;

    private System[] _systems;
    private Batcher _batcher;

    internal Core()
    {
        s_instance = this;
        s_gameWindow = Window;
        s_graphicsDeviceManager = new GraphicsDeviceManager(this);
        s_graphicsDevice = s_graphicsDeviceManager.GraphicsDevice;
    }

    internal static Core s_instance { get; private set; }
    internal static GraphicsDevice s_graphicsDevice { get; private set; }
    internal static GraphicsDeviceManager s_graphicsDeviceManager { get; private set; }
    internal static GameWindow s_gameWindow { get; private set; }

    protected sealed override void LoadContent()
    {
        base.LoadContent();

        _systems = new System[] { new Resources(), new Input(), new SceneManager() };
        _batcher = new Batcher();
    }

    protected sealed override void Initialize()
    {
        base.Initialize();

        OnPreInitialize?.Invoke();

        foreach (var system in _systems)
        {
            system.Initialize();
        }

        OnPostInitialize?.Invoke();
    }

    protected sealed override void Update(GameTime gameTime)
    {
        Time.Update(gameTime);

        foreach (var system in _systems)
        {
            system.PreUpdate();
        }

        base.Update(gameTime);

        foreach (var system in _systems)
        {
            system.PostUpdate();
        }

        OnUpdate?.Invoke();
    }

    protected sealed override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        base.Draw(gameTime);

        _batcher.Draw();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        _batcher.Dispose();

        foreach (var system in _systems)
        {
            system.Dispose();
        }
    }

    internal void Quit()
    {
        OnQuit?.Invoke();
        Exit();
    }
}
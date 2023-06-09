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
    public event Action OnStart;
    public event Action OnUpdate;
    public event Action OnQuit;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private System[] _systems;
    private Window _window;
    private Cursor _cursor;
    private Camera _camera;
    private Batcher _batcher;

    internal Core()
    {
        _graphics = new GraphicsDeviceManager(this);
        _window = new Window(this, Window, _graphics);
        _cursor = new Cursor();
        _camera = new Camera();
    }

    protected sealed override void LoadContent()
    {
        base.LoadContent();

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _batcher = new Batcher(_spriteBatch);
        _systems = new System[]
        {
            new Resources(),
            new Input(),
            new SceneManager()
        };
    }

    protected sealed override void Initialize()
    {
        base.Initialize();

        foreach(var system in _systems)
        {
            system.Initialize();
        }

        OnStart?.Invoke();
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
        _graphics.GraphicsDevice.Clear(Camera.BackgroundColor);

        base.Draw(gameTime);

        _batcher.Draw();
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        _window.Dispose();
        _cursor.Dispose();
        _camera.Dispose();
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
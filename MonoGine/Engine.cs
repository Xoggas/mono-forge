using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.InputSystem;
using MonoGine.Rendering;
using MonoGine.ResourceLoading;
using MonoGine.SceneManagement;
using System.Collections.Generic;

namespace MonoGine;

public abstract class Engine : Game
{
    private List<System> _systems;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Window _window;
    private Cursor _cursor;
    private Camera _camera;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        _window = new Window(base.Window, _graphics);
        _cursor = new Cursor();
        _camera = new Camera();
        _systems = new List<System>();
    }

    public new Window Window => _window;

    public void AddSystem(System system)
    {
        _systems.Add(system);
    }

    protected abstract void Start();
    protected abstract void Update();

    protected sealed override void BeginRun()
    {
        base.BeginRun();
    }

    protected sealed override bool BeginDraw()
    {
        return base.BeginDraw();
    }

    protected sealed override void EndDraw()
    {
        base.EndDraw();
    }

    protected sealed override void EndRun()
    {
        base.EndRun();
    }

    protected sealed override void LoadContent()
    {
        base.LoadContent();

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AddSystem(new Resources());
        AddSystem(new Input());
        AddSystem(new Batcher(_spriteBatch));
        AddSystem(new SceneManager());

        Start();
    }

    protected sealed override void UnloadContent()
    {
        base.UnloadContent();
    }

    protected sealed override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < _systems.Count; i++)
        {
            _systems[i].Initialize();
        }
    }

    protected sealed override void Update(GameTime gameTime)
    {
        for (int i = 0; i < _systems.Count; i++)
        {
            _systems[i].PreUpdate();
        }

        base.Update(gameTime);

        for (int i = 0; i < _systems.Count; i++)
        {
            _systems[i].PostUpdate();
        }

        Update();
    }

    protected sealed override void Draw(GameTime gameTime)
    {
        _graphics.GraphicsDevice.Clear(Camera.BackgroundColor);

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        _window.Dispose();
        _cursor.Dispose();
        _camera.Dispose();
    }
}
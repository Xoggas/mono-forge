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

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _window = new Window(Window);
        _cursor = new Cursor();
        _systems = new List<System>();
    }

    public void AddSystem(System system)
    {
        _systems.Add(system);
    }

    protected abstract void Start();
    protected abstract void Update();

    protected sealed override void BeginRun()
    {
        base.BeginRun();

        AddSystem(new Resources());
        AddSystem(new Input());
        AddSystem(new Batcher(_spriteBatch));
        AddSystem(new SceneManager());
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

        Start();
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
}
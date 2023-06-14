using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Graphics;
using MonoGine.ResourceLoading;
using MonoGine.Resources;
using MonoGine.SceneManagement;

namespace MonoGine;

public abstract class Engine : IObject
{
    private Core _core;
    private Time _time;
    private Screen _screen;
    private Window _window;
    private Cursor _cursor;
    private IResourceManager _resourceManager;
    private ISceneManager _sceneManager;
    private IBatcher _batcher;

    public Engine()
    {
        _core = new Core();
        _core.OnInitialize += OnInitialize;
        _core.OnLoadResources += OnLoadResources;
        _core.OnBeginDraw += OnBeginDraw;
        _core.OnDraw += OnDraw;
        _core.OnBeginUpdate += OnBeginUpdate;
        _core.OnUpdate += OnUpdate;

        _time = new Time();
        _screen = new Screen(_core);
        _window = new Window(_core);
        _cursor = new Cursor(_core);
        _resourceManager = new ResourceManager();
        _batcher = new DefaultBatcher();
        _sceneManager = new SceneManager();
    }

    public GraphicsDeviceManager GraphicsDeviceManager => _core.GraphicsDeviceManager;
    public GraphicsDevice GraphicsDevice => _core.GraphicsDevice;
    public Time Time => _time;
    public Screen Screen => _screen;
    public Window Window => _window;
    public Cursor Cursor => _cursor;
    public IResourceManager ResourceManager => _resourceManager;
    public ISceneManager SceneManager => _sceneManager;

    public virtual void OnInitialize()
    {
        _resourceManager.Initialize(this);
    }

    public virtual void OnLoadResources()
    {

    }

    public virtual void OnBeginUpdate(GameTime gameTime)
    {
        Time.Update(gameTime);
    }

    public virtual void OnUpdate(GameTime gameTime)
    {
        SceneManager.Update(this);
    }

    public virtual void OnBeginDraw(GameTime gameTime)
    {
        _batcher.Clear(this);
    }

    public virtual void OnDraw(GameTime gameTime)
    {
        _batcher.Draw(this);
    }

    public virtual void Dispose()
    {
        _time.Dispose();
        _window.Dispose();
        _cursor.Dispose();
        _resourceManager.Dispose();
        _batcher.Dispose();
        _sceneManager.Dispose();
    }

    public void Run()
    {
        _core.Run();
    }

    public void Exit()
    {
        _core.Exit();
    }
}
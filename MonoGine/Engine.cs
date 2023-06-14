using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Audio;
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
    private IAudioManager _audioManager;
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
        _audioManager = new AudioManager();
        _batcher = new Batcher();
        _sceneManager = new SceneManager();
    }

    public GraphicsDeviceManager GraphicsDeviceManager => _core.GraphicsDeviceManager;
    public GraphicsDevice GraphicsDevice => _core.GraphicsDevice;
    public Time Time => _time;
    public Screen Screen => _screen;
    public Window Window => _window;
    public Cursor Cursor => _cursor;
    public IResourceManager ResourceManager => _resourceManager;
    public IAudioManager AudioManager => _audioManager;
    public ISceneManager SceneManager => _sceneManager;

    public void Run()
    {
        _core.Run();
    }

    public void Exit()
    {
        _core.Exit();
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

    protected virtual void OnInitialize()
    {
        _resourceManager.Initialize(this);
        _resourceManager.RegisterProcessor<Texture2D>(new Texture2DProcessor());

        _audioManager.Initialize(this);
    }

    protected virtual void OnLoadResources()
    {

    }

    protected virtual void OnBeginUpdate(GameTime gameTime)
    {
        _time.Update(gameTime);
        _audioManager.Update(this);
    }

    protected virtual void OnUpdate(GameTime gameTime)
    {
        _sceneManager.Update(this);
    }

    protected virtual void OnBeginDraw(GameTime gameTime)
    {
        _batcher.Clear(this);
    }

    protected virtual void OnDraw(GameTime gameTime)
    {
        _batcher.Draw(this);
    }
}
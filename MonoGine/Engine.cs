using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Audio;
using MonoGine.Graphics;
using MonoGine.ResourceLoading;
using MonoGine.Resources;
using MonoGine.SceneManagement;

namespace MonoGine;

/// <summary>
/// Represents the base class for the game engine.
/// </summary>
public abstract class Engine : IEngine
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

    /// <summary>
    /// Initializes a new instance of the Engine class.
    /// </summary>
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
        _window = new Window(_core, this);
        _cursor = new Cursor(_core);
        _resourceManager = new ResourceManager();
        _audioManager = new AudioManager();
        _batcher = new Batcher();
        _sceneManager = new SceneManager();
    }

    /// <summary>
    /// Gets the graphics device manager associated with the engine.
    /// </summary>
    public GraphicsDeviceManager GraphicsDeviceManager => _core.GraphicsDeviceManager;

    /// <summary>
    /// Gets the graphics device associated with the engine.
    /// </summary>
    public GraphicsDevice GraphicsDevice => _core.GraphicsDevice;

    /// <summary>
    /// Gets the time instance associated with the engine.
    /// </summary>
    public Time Time => _time;

    /// <summary>
    /// Gets the screen instance associated with the engine.
    /// </summary>
    public Screen Screen => _screen;

    /// <summary>
    /// Gets the window instance associated with the engine.
    /// </summary>
    public Window Window => _window;

    /// <summary>
    /// Gets the cursor instance associated with the engine.
    /// </summary>
    public Cursor Cursor => _cursor;

    /// <summary>
    /// Gets the resource manager instance associated with the engine.
    /// </summary>
    public IResourceManager ResourceManager => _resourceManager;

    /// <summary>
    /// Gets the audio manager instance associated with the engine.
    /// </summary>
    public IAudioManager AudioManager => _audioManager;

    /// <summary>
    /// Gets the scene manager instance associated with the engine.
    /// </summary>
    public ISceneManager SceneManager => _sceneManager;

    /// <summary>
    /// Runs the engine.
    /// </summary>
    public void Run()
    {
        _core.Run();
    }

    /// <summary>
    /// Exits the engine.
    /// </summary>
    public void Exit()
    {
        _core.Exit();
    }

    /// <summary>
    /// Disposes the engine and releases any resources it holds.
    /// </summary>
    public virtual void Dispose()
    {
        _time.Dispose();
        _window.Dispose();
        _cursor.Dispose();
        _resourceManager.Dispose();
        _batcher.Dispose();
        _sceneManager.Dispose();
    }

    /// <summary>
    /// Handles the initialization of the engine.
    /// </summary>
    protected virtual void OnInitialize()
    {
        _resourceManager.Initialize(this);
        _resourceManager.RegisterProcessor<Effect>(new EffectProcessor());
        _resourceManager.RegisterProcessor<Texture2D>(new Texture2DProcessor());

        _audioManager.Initialize(this);
    }

    /// <summary>
    /// Handles the loading of resources.
    /// </summary>
    protected virtual void OnLoadResources()
    {
        // Implementation of resource loading.
    }

    /// <summary>
    /// Handles the beginning of the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnBeginUpdate(GameTime gameTime)
    {
        _time.Update(gameTime);
        _audioManager.Update(this);
    }

    /// <summary>
    /// Handles the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnUpdate(GameTime gameTime)
    {
        _sceneManager.Update(this);
    }

    /// <summary>
    /// Handles the beginning of the draw phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnBeginDraw(GameTime gameTime)
    {
        _batcher.Begin(this);
    }

    /// <summary>
    /// Handles the draw phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnDraw(GameTime gameTime)
    {
        _sceneManager.Draw(this, _batcher);
        _batcher.End(this);
    }
}

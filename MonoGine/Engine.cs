using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Audio;
using MonoGine.InputSystem;
using MonoGine.Rendering;
using MonoGine.ResourceLoading;
using MonoGine.SceneManagement;

namespace MonoGine;

/// <summary>
///     Represents the base class for the game engine.
/// </summary>
public abstract class Engine : IEngine
{
    private readonly Core _core;

    /// <summary>
    ///     Initializes a new instance of the Engine class.
    /// </summary>
    protected Engine()
    {
        _core = new Core();
        _core.OnInitialize += OnInitialize;
        _core.OnLoadResources += OnLoadResources;
        _core.OnUnloadResources += OnUnloadResources;
        _core.OnDraw += OnDraw;
        _core.OnBeginUpdate += OnBeginUpdate;
        _core.OnUpdate += OnUpdate;

        Time = new Time();
        Screen = new Screen(_core);
        Cursor = new Cursor(_core);
        Input = new Input(_core.Window);
        SceneManager = new SceneManager();
        AudioManager = new AudioManager();
        ResourceManager = new ResourceManager(this);
    }

    /// <summary>
    ///     Gets the graphics device manager associated with the engine.
    /// </summary>
    public GraphicsDeviceManager GraphicsDeviceManager => _core.GraphicsDeviceManager;

    /// <summary>
    ///     Gets the graphics device associated with the engine.
    /// </summary>
    public GraphicsDevice GraphicsDevice => _core.GraphicsDevice;

    /// <summary>
    ///     Gets the time instance associated with the engine.
    /// </summary>
    public Time Time { get; }

    /// <summary>
    ///     Gets the screen instance associated with the engine.
    /// </summary>
    public Screen Screen { get; }

    /// <summary>
    ///     Gets the window instance associated with the engine.
    /// </summary>
    public Window Window { get; private set; } = default!;

    /// <summary>
    ///     Gets the input provider.
    /// </summary>
    public IInput Input { get; }

    /// <summary>
    ///     Gets the cursor instance associated with the engine.
    /// </summary>
    public Cursor Cursor { get; }

    /// <summary>
    ///     Gets or sets the resource manager instance associated with the engine.
    /// </summary>
    public IResourceManager ResourceManager { get; protected set; }

    /// <summary>
    ///     Gets or sets the scene manager instance associated with the engine.
    /// </summary>
    public ISceneManager SceneManager { get; protected set; }

    /// <summary>
    ///     Gets or sets the audio manager instance associated with the engine.
    /// </summary>
    public IAudioManager AudioManager { get; protected set; }

    /// <summary>
    ///     Gets the renderer.
    /// </summary>
    public IRenderer Renderer { get; protected set; } = default!;

    /// <summary>
    ///     Exits the engine.
    /// </summary>
    public void Exit()
    {
        _core.Exit();
    }

    /// <summary>
    ///     Disposes the engine and releases any resources it holds.
    /// </summary>
    public virtual void Dispose()
    {
        Time.Dispose();
        Window.Dispose();
        Input.Dispose();
        Cursor.Dispose();
        ResourceManager.Dispose();
        SceneManager.Dispose();
        AudioManager.Dispose();
        Renderer.Dispose();
    }

    /// <summary>
    ///     Runs the engine.
    /// </summary>
    public void Run()
    {
        _core.Run();
    }

    /// <summary>
    ///     Handles the initialization of the engine.
    /// </summary>
    protected virtual void OnInitialize()
    {
        Window = new Window(_core);
        Renderer = new Renderer(this);
        AudioManager.Initialize(this);
        ResourceManager.Initialize(this);
    }

    /// <summary>
    ///     Handles the loading of resources.
    /// </summary>
    protected virtual void OnLoadResources()
    {
    }

    /// <summary>
    ///     Handles the unloading of resources.
    /// </summary>
    protected virtual void OnUnloadResources()
    {
    }

    /// <summary>
    ///     Handles the beginning of the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnBeginUpdate(GameTime gameTime)
    {
        AudioManager.Update(this);
    }

    /// <summary>
    ///     Handles the update phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnUpdate(GameTime gameTime)
    {
        Time.Update(gameTime);
        Input.Update(this);
        SceneManager.Update(this);
    }

    /// <summary>
    ///     Handles the draw phase.
    /// </summary>
    /// <param name="gameTime">The game time.</param>
    protected virtual void OnDraw(GameTime gameTime)
    {
        if (Renderer == null)
        {
            throw new NullReferenceException("The renderer wasn't set");
        }

        if (SceneManager.CurrentScene == null)
        {
            return;
        }

        Renderer.Draw(this, SceneManager.CurrentScene);
    }
}
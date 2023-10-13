using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Audio;
using MonoGine.InputSystem;
using MonoGine.Rendering;
using MonoGine.ResourceLoading;
using MonoGine.SceneManagement;

namespace MonoGine;

/// <summary>
/// Represents the game engine interface in the MonoGine framework.
/// </summary>
public interface IEngine : IObject
{
    /// <summary>
    /// Gets the graphics device manager associated with the engine.
    /// </summary>
    public GraphicsDeviceManager GraphicsDeviceManager { get; }

    /// <summary>
    /// Gets the graphics device associated with the engine.
    /// </summary>
    public GraphicsDevice GraphicsDevice { get; }

    /// <summary>
    /// Gets the time information of the engine.
    /// </summary>
    public Time Time { get; }

    /// <summary>
    /// Gets the screen information of the engine.
    /// </summary>
    public Screen Screen { get; }

    /// <summary>
    /// Gets the window information of the engine.
    /// </summary>
    public Window Window { get; }

    /// <summary>
    /// Gets the input provider.
    /// </summary>
    public IInput Input { get; }

    /// <summary>
    /// Gets the cursor information of the engine.
    /// </summary>
    public Cursor Cursor { get; }

    /// <summary>
    /// Gets the resource manager associated with the engine.
    /// </summary>
    public IResourceManager ResourceManager { get; }

    /// <summary>
    /// Gets the scene manager associated with the engine.
    /// </summary>
    public ISceneManager SceneManager { get; }

    /// <summary>
    /// Gets the audio manager associated with the engine.
    /// </summary>
    public IAudioManager AudioManager { get; }

    /// <summary>
    /// Gets the renderer.
    /// </summary>
    public IRenderer Renderer { get; }

    /// <summary>
    /// Exits the game or application.
    /// </summary>
    public void Exit();
}
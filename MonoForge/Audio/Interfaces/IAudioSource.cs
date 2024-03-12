namespace MonoForge.Audio;

public interface IAudioSource : IObject, IUpdatable, IDestroyable
{
    /// <summary>
    /// Gets the channel Audio Source belongs to.
    /// </summary>
    public IAudioChannel Channel { get; }

    /// <summary>
    /// Gets the current Audio Clip.
    /// </summary>
    public IAudioClip? Clip { get; set; }

    /// <summary>
    /// Gets or sets the id of Audio Source.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// Gets the current playback state.
    /// </summary>
    public bool IsPlaying { get; }

    /// <summary>
    /// Gets the Audio Source time.
    /// </summary>
    public float Time { get; set; }

    /// <summary>
    /// Gets or sets the volume.
    /// </summary>
    public float Volume { get; set; }

    /// <summary>
    /// Gets or sets the current playback speed.
    /// </summary>
    public float Pitch { get; set; }

    /// <summary>
    /// Gets or sets IsLooping flag.
    /// </summary>
    public bool IsLooping { get; set; }
    
    /// <summary>
    /// Gets the destroyed state.
    /// </summary>
    public bool IsDestroyed { get; }

    /// <summary>
    /// Starts the playback.
    /// </summary>
    public void Play();

    /// <summary>
    /// Pauses the playback.
    /// </summary>
    public void Pause();

    /// <summary>
    /// Stops the playback.
    /// </summary>
    public void Stop();
}

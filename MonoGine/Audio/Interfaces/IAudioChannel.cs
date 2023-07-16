using System;

namespace MonoGine.Audio;

public interface IAudioChannel : IObject, IUpdatable
{
    /// <summary>
    /// Gets or sets the channel volume.
    /// </summary>
    public float Volume { get; set; }
    
    /// <summary>
    /// Gets or sets the channel speed.
    /// </summary>
    public float Pitch { get; set; }

    /// <summary>
    /// Returns the new Audio Source.
    /// </summary>
    public IAudioSource CreateSource();

    /// <summary>
    /// Removes the Audio Source.
    /// </summary>
    /// <param name="source">A source to remove.</param>
    public void RemoveSource(IAudioSource source);
    
    public void PauseById(string id, StringComparison comparison);

    public void StopById(string id, StringComparison comparison);

    public void DestroyById(string id, StringComparison comparison);
    
    public void PauseAll();

    public void StopAll();

    public void DestroyAll();
}

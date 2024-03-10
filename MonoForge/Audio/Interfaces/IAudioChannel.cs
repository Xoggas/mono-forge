using System;

namespace MonoForge.Audio;

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
    
    /// <summary>
    /// Pauses all Audio Sources by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to pause.</param>
    /// <param name="comparison">String comparator.</param>
    public void PauseById(string id, StringComparison comparison);

    /// <summary>
    /// Stops all Audio Sources by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to stop.</param>
    /// <param name="comparison">String comparator.</param>
    public void StopById(string id, StringComparison comparison);

    /// <summary>
    /// Destroys all Audio Sources by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to stop.</param>
    /// <param name="comparison">String comparator.</param>
    public void DestroyById(string id, StringComparison comparison);
    
    /// <summary>
    /// Pauses all Audio Sources.
    /// </summary>
    public void PauseAll();

    /// <summary>
    /// Stops all Audio Sources.
    /// </summary>
    public void StopAll();

    /// <summary>
    /// Destroys all Audio Sources.
    /// </summary>
    public void DestroyAll();
}

using System;

namespace MonoForge.Audio;

public interface IAudioManager : ISystem
{
    /// <summary>
    /// Gets the master channel.
    /// </summary>
    public IAudioChannel Master { get; }
    
    /// <summary>
    /// Gets the Sfx channel.
    /// </summary>
    public IAudioChannel Sfx { get; }

    /// <summary>
    /// Gets or adds a new sound channel.
    /// </summary>
    /// <param name="id">Id of the channel to get. 0 is set for master and 1 for sfx channel by default.</param>
    /// <returns></returns>
    public IAudioChannel GetOrAddChannel(int id);
    
    /// <summary>
    /// Pauses all Audio Sources in all channels by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to pause.</param>
    /// <param name="comparison">String comparator.</param>
    public void PauseById(string id, StringComparison comparison);

    /// <summary>
    /// Stops all Audio Sources in all channels by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to stop.</param>
    /// <param name="comparison">String comparator.</param>
    public void StopById(string id, StringComparison comparison);

    /// <summary>
    /// Destroys all Audio Sources in all channels by the specified id.
    /// </summary>
    /// <param name="id">The id of sources to destroy.</param>
    /// <param name="comparison">String comparator.</param>
    public void DestroyById(string id, StringComparison comparison);
    
    /// <summary>
    /// Pauses all Audio Sources in all channels.
    /// </summary>
    public void PauseAll();

    /// <summary>
    /// Stops all Audio Sources in all channels.
    /// </summary>
    public void StopAll();

    /// <summary>
    /// Destroys all Audio Sources in all channels.
    /// </summary>
    public void DestroyAll();
}

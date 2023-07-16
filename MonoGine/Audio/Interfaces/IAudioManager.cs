using System;

namespace MonoGine.Audio;

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
    
    public void PauseById(string id, StringComparison comparison);

    public void StopById(string id, StringComparison comparison);

    public void DestroyById(string id, StringComparison comparison);
    
    public void PauseAll();

    public void StopAll();

    public void DestroyAll();
}

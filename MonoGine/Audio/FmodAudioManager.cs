using System;

namespace MonoGine.Audio;

public sealed class FmodAudioManager : IAudioManager
{
    internal FmodAudioManager(IEngine engine)
    {
        Master = default!;
        Sfx = default!;
    }

    public IAudioChannel Master { get; }
    public IAudioChannel Sfx { get; }

    public void Update(IEngine engine)
    {
        
    }

    public IAudioChannel GetOrAddChannel(int id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
}

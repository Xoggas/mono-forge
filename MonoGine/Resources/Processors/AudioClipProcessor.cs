using System;
using FmodForFoxes;
using MonoGine.Audio;

namespace MonoGine.Resources;

public sealed class AudioClipProcessor : IProcessor<AudioClip>
{
    public AudioClip Load(IEngine engine, string path)
    {
        return new AudioClip(CoreSystem.LoadStreamedSound(path));
    }

    public void Save(IEngine engine, string path, AudioClip resource)
    {
        throw new InvalidOperationException("Audio Clip saving is not supported!");
    }
}

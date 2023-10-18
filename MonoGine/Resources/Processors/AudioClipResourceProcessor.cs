using FmodForFoxes;
using MonoGine.Audio;

namespace MonoGine.ResourceLoading;

public sealed class AudioClipResourceProcessor : IResourceReader<AudioClip>
{
    public AudioClip Read(IEngine engine, string path)
    {
        return new AudioClip(CoreSystem.LoadStreamedSound(path));
    }
}
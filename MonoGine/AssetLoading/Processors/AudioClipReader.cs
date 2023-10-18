using FmodForFoxes;
using MonoGine.Audio;

namespace MonoGine.AssetLoading;

public sealed class AudioClipReader : IAssetReader<AudioClip>
{
    public AudioClip Read(IEngine engine, string path)
    {
        return new AudioClip(CoreSystem.LoadStreamedSound(path));
    }
}
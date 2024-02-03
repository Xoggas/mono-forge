using FmodForFoxes;
using MonoGine.Audio;

namespace MonoGine.AssetLoading;

internal sealed class AudioClipReader : IAssetReader<AudioClip>
{
    public AudioClip Read(IEngine engine, string localPath)
    {
        return new AudioClip(CoreSystem.LoadStreamedSound(localPath));
    }
}
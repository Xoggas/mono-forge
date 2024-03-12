using FmodForFoxes;

namespace MonoForge.Audio;

public sealed class AudioClip : IAudioClip
{
    private readonly Sound _sound;

    private AudioClip(Sound sound)
    {
        _sound = sound;
    }

    public static AudioClip FromFile(string path)
    {
        return new AudioClip(CoreSystem.LoadSound(path));
    }

    public static AudioClip FromFileStreamed(string path)
    {
        return new AudioClip(CoreSystem.LoadStreamedSound(path));
    }

    public uint DurationInMilliseconds => _sound.Length;
    public float DurationInSeconds => _sound.Length * 0.001f;

    public void Dispose()
    {
        _sound.Dispose();
    }

    internal Channel GetFmodChannel()
    {
        return _sound.Play(true);
    }
}
using FmodForFoxes;

namespace MonoGine.Audio;

public sealed class AudioClip : IAudioClip
{
    private readonly Sound _sound;

    internal AudioClip(Sound sound)
    {
        _sound = sound;
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

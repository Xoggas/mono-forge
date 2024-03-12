namespace MonoForge.Audio;

//TODO: Implement 3D sound
public sealed class AudioSource : IAudioSource
{
    private readonly FmodChannel _fmodChannel;

    internal AudioSource(IAudioChannel channel)
    {
        Channel = channel;
        Id = string.Empty;
        _fmodChannel = new FmodChannel();
    }

    public IAudioChannel Channel { get; }

    public IAudioClip? Clip
    {
        get => _fmodChannel.Clip;
        set => _fmodChannel.Clip = value as AudioClip;
    }

    public float Time
    {
        get => _fmodChannel.Time;
        set => _fmodChannel.Time = value;
    }

    public bool IsLooping
    {
        get => _fmodChannel.IsLooping;
        set => _fmodChannel.IsLooping = value;
    }

    public string Id { get; set; }
    public bool IsPlaying => _fmodChannel.IsPlaying;
    public float Volume { get; set; } = 1f;
    public float Pitch { get; set; } = 1f;
    public bool IsDestroyed { get; private set; }

    public void Update(GameBase gameBase, float deltaTime)
    {
        _fmodChannel.Volume = Channel.Volume * Volume;
        _fmodChannel.Pitch = Channel.Pitch * Pitch;
    }

    public void Play()
    {
        _fmodChannel.Play();
    }

    public void Pause()
    {
        _fmodChannel.Pause();
    }

    public void Stop()
    {
        _fmodChannel.Stop();
    }

    public void Destroy()
    {
        Stop();
        Dispose();
        IsDestroyed = true;
    }

    public void Dispose()
    {
        _fmodChannel.Dispose();
    }
}
namespace MonoForge.Audio;

public sealed class AudioClip : IAudioClip
{
    public uint DurationInMilliseconds { get; }
    public float DurationInSeconds { get; }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}
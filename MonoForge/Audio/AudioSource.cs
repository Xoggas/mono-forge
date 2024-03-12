using System;

namespace MonoForge.Audio;

//TODO: Implement 3D sound
public sealed class AudioSource : IAudioSource
{
    public IAudioChannel Channel { get; }
    public IAudioClip? Clip { get; set; }
    public string Id { get; set; }
    public bool IsPlaying { get; }
    public float Time { get; set; }
    public float Volume { get; set; }
    public float Pitch { get; set; }
    public bool IsLooping { get; set; }

    public bool IsDestroyed { get; }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Update(GameBase gameBase, float deltaTime)
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
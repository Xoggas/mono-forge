using System;
using FmodForFoxes;

namespace MonoGine.Audio;

internal sealed class FmodChannel : IObject
{
    private Channel _channel;
    private AudioClip? _clip;

    internal AudioClip? Clip
    {
        get => _clip;
        set
        {
            if (_channel.Sound != null)
            {
                _channel.Stop();
            }

            if (value != null)
            {
                _channel = value.GetFmodChannel();
                _channel.Volume = 0f;
                _channel.Looping = IsLooping;
                _clip = value;
            }
            else
            {
                _clip = null;
            }
        }
    }

    internal bool IsPlaying => TryGetProperty(channel => channel.IsPlaying, false);

    internal bool IsLooping
    {
        get => TryGetProperty(channel => channel.Looping, false);
        set => TrySetProperty(channel => channel.Looping = value);
    }

    internal uint RawTime => TryGetProperty(channel => channel.TrackPosition, 0u);

    internal float Time
    {
        get => TryGetProperty(channel => channel.TrackPosition * 0.001f, 0f);
        set => TrySetProperty(channel => channel.TrackPosition = (uint)(value * 1000u));
    }

    internal float Volume
    {
        get => TryGetProperty(channel => channel.Volume, 1f);
        set => TrySetProperty(channel => channel.Volume = value);
    }

    internal float Pitch
    {
        get => TryGetProperty(channel => channel.Pitch, 1f);
        set => TrySetProperty(channel => channel.Pitch = value);
    }

    public void Dispose()
    {
        _clip = null;
    }

    internal void Play()
    {
        if (_channel.Sound == null && _clip != null)
        {
            Clip = _clip;
        }

        if (_channel.Sound != null)
        {
            _channel.Resume();
        }
    }

    internal void Pause()
    {
        if (_channel.Sound != null)
        {
            _channel.Pause();
        }
    }

    internal void Stop()
    {
        if (_channel.Sound != null)
        {
            Pause();
            Time = 0f;
        }
    }

    private T TryGetProperty<T>(Func<Channel, T> predicate, T defaultValue)
    {
        return _channel.Sound != null ? predicate.Invoke(_channel) : defaultValue;
    }

    private void TrySetProperty(Action<Channel> predicate)
    {
        if (_channel.Sound != null)
        {
            predicate.Invoke(_channel);
        }
    }
}

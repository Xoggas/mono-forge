﻿namespace MonoGine.Audio;

public interface IAudioClip : IObject
{
    /// <summary>
    /// Gets the duration of the clip in milliseconds.
    /// </summary>
    public uint DurationInMilliseconds { get; }

    /// <summary>
    ///  Gets the duration of the clip in seconds.
    /// </summary>
    public float DurationInSeconds { get; }
}
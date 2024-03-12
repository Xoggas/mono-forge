using System;
using System.Collections.Generic;

namespace MonoForge.Audio;

public sealed class AudioManager : IAudioManager
{
    private readonly Dictionary<int, IAudioChannel> _channels;

    internal AudioManager()
    {
        _channels = new Dictionary<int, IAudioChannel>
        {
            { 0, Master },
            { 1, Sfx }
        };
    }

    public IAudioChannel Master { get; } = new AudioChannel();
    public IAudioChannel Sfx { get; } = new AudioChannel();

    public void Update(GameBase gameBase, float deltaTime)
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.Update(gameBase, deltaTime));
    }

    public IAudioChannel GetOrAddChannel(int id)
    {
        if (_channels.TryGetValue(id, out IAudioChannel? value))
        {
            return value;
        }

        var channel = new AudioChannel();

        _channels.Add(id, channel);

        return channel;
    }

    public void PauseById(string id, StringComparison comparison)
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.PauseById(id, comparison));
    }

    public void PauseAll()
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.PauseAll());
    }

    public void StopById(string id, StringComparison comparison)
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.StopById(id, comparison));
    }

    public void StopAll()
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.StopAll());
    }

    public void DestroyById(string id, StringComparison comparison)
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.DestroyById(id, comparison));
    }

    public void DestroyAll()
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.DestroyAll());
    }

    public void Dispose()
    {
        ExecuteCallbackForChannels(_channels.Values, channel => channel.Dispose());
    }

    private static void ExecuteCallbackForChannels(IEnumerable<IAudioChannel> channels, Action<IAudioChannel> callback)
    {
        foreach (IAudioChannel channel in channels)
        {
            callback.Invoke(channel);
        }
    }
}
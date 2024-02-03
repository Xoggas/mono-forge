using System;
using System.Collections.Generic;
using System.Linq;
using MonoGine.AssetLoading;
using Newtonsoft.Json;

namespace MonoGine.Animations;

[Serializable]
public class AnimationClip : IAsset
{
    [JsonProperty]
    private Dictionary<string, Sequence> _sequences = new();

    [JsonProperty]
    private float _duration;

    public AnimationClip(Dictionary<string, Sequence> sequences)
    {
        _sequences = sequences;
        _duration = sequences.Values.Max(x => x.Duration);
    }

    private AnimationClip()
    {
    }

    [JsonIgnore]
    public IReadOnlyDictionary<string, Sequence> Sequences => _sequences;

    [JsonIgnore]
    public float Duration => _duration;

    public void Dispose()
    {
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoForge.Animations;

[Serializable]
public class AnimationClip
{
    public IReadOnlyDictionary<string, Sequence> Sequences => _sequences;
    public float Duration { get; }

    private Dictionary<string, Sequence> _sequences = new();

    public AnimationClip(Dictionary<string, Sequence> sequences)
    {
        Duration = sequences.Values.Max(x => x.Duration);
        _sequences = sequences;
    }

    private AnimationClip()
    {
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoForge.Animations;

[Serializable]
public class AnimationClip
{
    public Dictionary<string, Sequence> Sequences { get; }
    public float Duration { get; }

    public AnimationClip(Dictionary<string, Sequence> sequences)
    {
        Duration = sequences.Values.Max(x => x.Duration);
        Sequences = sequences;
    }
}
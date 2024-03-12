using System.Collections.Generic;
using System.Linq;

namespace MonoForge.Animations;

public sealed class AnimationClip
{
    public IReadOnlyDictionary<string, Sequence> Sequences { get; }
    public float Duration { get; }

    public AnimationClip(IReadOnlyDictionary<string, Sequence> sequences)
    {
        Duration = sequences.Values.Max(x => x.Duration);
        Sequences = sequences;
    }
}
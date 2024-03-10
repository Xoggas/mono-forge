using System;

namespace MonoForge.Animations;

internal sealed class AnimatorBinding
{
    private readonly Sequence _sequence;
    private readonly Action<float> _setter;

    internal AnimatorBinding(Sequence sequence, Action<float> setter)
    {
        _sequence = sequence;
        _setter = setter;
    }

    internal void Update(float time)
    {
        _setter.Invoke(_sequence.Evaluate(time));
    }
}
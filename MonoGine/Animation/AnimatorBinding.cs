using System;

namespace MonoGine.Animations;

internal sealed class AnimatorBinding
{
    private readonly Sequence _sequence;
    private readonly Action<float> _setter;

    public AnimatorBinding(Sequence sequence, Action<float> setter)
    {
        _sequence = sequence;
        _setter = setter;
    }

    public void Animate(float time)
    {
        _setter.Invoke(_sequence.Evaluate(time));
    }
}
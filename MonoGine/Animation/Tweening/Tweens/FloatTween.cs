using System;

namespace MonoGine.Animations.Tweening;

public sealed class FloatTween : Tween<float>
{
    public FloatTween(float startValue, float endValue, float duration, Action<float> setter) : base(startValue,
        endValue, duration, setter)
    {
    }

    protected override float Interpolate(float startValue, float endValue, float progress)
    {
        return EasingFunctions.GetEasingFunction(Ease).Invoke(startValue, endValue, progress);
    }
}
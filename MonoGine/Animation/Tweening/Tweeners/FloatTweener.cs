namespace MonoGine.Animations.Tweening;

public sealed class FloatTweener : ITweener<float>
{
    public float Evaluate(Ease ease, float s, float e, float v)
    {
        return EasingFunctions.GetEasingFunction(ease).Invoke(s, e, v);
    }
}
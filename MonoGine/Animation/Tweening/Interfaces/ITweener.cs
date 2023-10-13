namespace MonoGine.Animations.Tweening;

public interface ITweener
{
}

public interface ITweener<out T> : ITweener
{
    public T Evaluate(Ease ease, float s, float e, float v);
}
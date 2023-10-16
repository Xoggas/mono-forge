using System;
using Microsoft.Xna.Framework;
using MonoGine.Ecs;

namespace MonoGine.Animations.Tweening;

public abstract class Tween<T> : Component where T : struct
{
    private const int InfiniteLoop = -1;
    private const float MinFloatDifference = 0.00001f;

    private float _lastTime;
    private readonly T _startValue;
    private readonly T _endValue;
    private readonly Action<T> _setter;

    private Action? _played;
    private Action? _paused;
    private Action? _stopped;
    private Action? _completed;
    private Action? _killed;

    protected Tween(T startValue, T endValue, float duration, Action<T> setter)
    {
        Duration = duration;
        _startValue = startValue;
        _endValue = endValue;
        _setter = setter;
    }

    public float Time { get; set; }
    public float Duration { get; }
    public float Speed { get; set; } = 1f;
    public bool IsPlaying { get; set; }
    public bool AutoKillOnEnd { get; set; } = true;
    public Ease Ease { get; private set; } = Ease.Linear;
    public int Loops { get; private set; } = 1;
    public LoopType LoopType { get; private set; }

    public override void Start(IEngine engine)
    {
        base.Start(engine);

        switch (Duration)
        {
            case < 0:
                throw new ArgumentException("Duration can't be less or equal than zero");
            case 0f:
                Kill(true);
                break;
        }
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        if (IsPlaying)
        {
            Time += engine.Time.DeltaTime * Speed;
        }

        UpdateTimeAndLoops();
    }

    public Tween<T> SetEase(Ease ease)
    {
        Ease = ease;
        return this;
    }

    public Tween<T> SetLoops(int loops, LoopType loopType = LoopType.Repeat)
    {
        Loops = loops;
        LoopType = loopType;
        return this;
    }

    public Tween<T> Play()
    {
        IsPlaying = true;

        _played?.Invoke();

        return this;
    }

    public Tween<T> OnPlay(Action callback)
    {
        _played = callback;
        return this;
    }

    public Tween<T> Pause()
    {
        IsPlaying = false;

        _paused?.Invoke();

        return this;
    }

    public Tween<T> OnPause(Action callback)
    {
        _paused = callback;
        return this;
    }

    public Tween<T> Stop()
    {
        IsPlaying = false;
        Time = 0f;

        _stopped?.Invoke();

        return this;
    }

    public Tween<T> OnStop(Action callback)
    {
        _stopped = callback;
        return this;
    }

    public Tween<T> Complete()
    {
        Pause();
        Time = Duration;
        UpdateTimeAndLoops();

        _completed?.Invoke();

        return this;
    }

    public Tween<T> Restart()
    {
        Time = 0f;
        Play();
        return this;
    }

    public Tween<T> OnComplete(Action callback)
    {
        _completed = callback;
        return this;
    }

    public Tween<T> Kill(bool complete = false)
    {
        if (complete)
        {
            Complete();
        }

        Destroy();

        _killed?.Invoke();

        return this;
    }

    public Tween<T> OnKill(Action callback)
    {
        _killed = callback;
        return this;
    }

    protected abstract T Interpolate(T startValue, T endValue, float progress);

    //TODO: Refactor this method
    private void UpdateTimeAndLoops()
    {
        if (Math.Abs(_lastTime - Time) < MinFloatDifference)
        {
            return;
        }

        _lastTime = Time;
        _setter.Invoke(Interpolate(_startValue, _endValue, Time / Duration));

        if (Time >= 0f && Time <= Duration)
        {
            return;
        }

        if (LoopType is LoopType.PingPong)
        {
            Speed *= -1;
        }
        else if (Time < 0f)
        {
            Time = Duration;
        }
        else if (Time > Duration)
        {
            Time = 0f;
        }
        else
        {
            Time = MathHelper.Clamp(Time, 0f, Duration);
        }

        if (Loops != InfiniteLoop)
        {
            Loops--;
        }

        if (Loops != 0)
        {
            return;
        }

        if (AutoKillOnEnd)
        {
            Kill();
        }

        _completed?.Invoke();
    }
}
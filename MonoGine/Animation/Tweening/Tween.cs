using System;
using MonoGine.Ecs;

namespace MonoGine.Animations.Tweening;

public abstract class Tween<T> : ITween
{
    private TweenGetter<T> _getter;
    private TweenSetter<T> _setter;

    protected Tween(IEntity entity, TweenGetter<T> getter, TweenSetter<T> setter)
    {
        Entity = entity;
        _getter = getter;
        _setter = setter;
    }

    public event TweenCallback? OnPlay;
    public event TweenCallback? OnPause;
    public event TweenCallback? OnStop;
    public event TweenCallback? OnComplete;
    public event TweenCallback? OnKill;

    public IEntity? Entity { get; }

    public bool Started { get; }

    public bool IsDestroyed { get; }

    public bool IsActive { get; }

    public float Time { get; set; }

    public float Duration { get; }

    public float Speed { get; set; }

    public bool IsPlaying { get; set; }

    public bool AutoKillOnEnd { get; set; }

    
    public void Start(IEngine engine)
    {
        throw new NotImplementedException();
    }
    
    public void Update(IEngine engine)
    {
        throw new NotImplementedException();
    }

    public void SetLoops(int loops, LoopType loopType = LoopType.Repeat)
    {
        throw new NotImplementedException();
    }

    public void Play()
    {
        throw new NotImplementedException();
    }

    public void Pause()
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void Destroy()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
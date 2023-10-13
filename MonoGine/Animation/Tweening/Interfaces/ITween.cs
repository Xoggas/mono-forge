using MonoGine.Ecs;

namespace MonoGine.Animations.Tweening;

public delegate void TweenCallback();

public delegate T TweenGetter<out T>();

public delegate void TweenSetter<in T>(T value);

public interface ITween : IComponent
{
    public event TweenCallback? OnPlay;

    public event TweenCallback? OnPause;

    public event TweenCallback? OnStop;

    public event TweenCallback? OnComplete;

    public event TweenCallback? OnKill;

    public float Time { get; set; }

    public float Duration { get; }

    public float Speed { get; set; }

    public bool IsPlaying { get; set; }
    
    public bool AutoKillOnEnd { get; set; }
    
    public void SetLoops(int loops, LoopType loopType = LoopType.Repeat);

    public void Play();

    public void Pause();

    public void Stop();
}

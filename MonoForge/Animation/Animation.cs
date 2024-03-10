using System;
using System.Collections.Generic;
using MonoForge.Ecs;

namespace MonoForge.Animations;

//TODO: Implement looping
public sealed class Animation : Component
{
    private readonly IAnimatable _target;
    private readonly List<AnimatorBinding> _bindings;

    public Animation(IAnimatable target)
    {
        _target = target;
        _bindings = new List<AnimatorBinding>();
    }

    public bool IsPlaying { get; private set; }
    public float Time { get; set; }
    public float Speed { get; set; } = 1f;
    public AnimationClip? Clip { get; private set; }

    public void Play()
    {
        IsPlaying = true;
    }

    public void Pause()
    {
        IsPlaying = false;
    }

    public void Stop()
    {
        IsPlaying = false;
        Time = 0f;
    }

    public void SetClip(AnimationClip? clip)
    {
        _bindings.Clear();
        Clip = clip;
        CreateBindings();
    }

    public override void Update(IGame game, float deltaTime)
    {
        base.Update(game, deltaTime);

        if (IsPlaying == false || Clip is null)
        {
            return;
        }

        Time += game.Time.DeltaTime * Speed;

        if (Time < 0f)
        {
            Time = Clip.Duration;
        }

        if (Time >= Clip.Duration)
        {
            Time = 0f;
        }

        UpdateBindings();
    }

    private void UpdateBindings()
    {
        foreach (AnimatorBinding binding in _bindings)
        {
            binding.Update(Time);
        }
    }

    //TODO: Refactor to get rid of redundant memory allocations
    private void CreateBindings()
    {
        if (Clip is null)
        {
            return;
        }

        foreach ((var path, Sequence sequence) in Clip.Sequences)
        {
            var paths = path.Split('/');
            var propertyName = paths[^1];

            if (paths.Length == 1)
            {
                _bindings.Add(new AnimatorBinding(sequence, _target.GetPropertySetter(propertyName)));
            }
            else
            {
                IAnimatable? child = _target;

                for (var i = 0; i < paths.Length - 1; i++)
                {
                    child = child?.GetChild(paths[i]);
                }

                if (child is not null)
                {
                    _bindings.Add(new AnimatorBinding(sequence, child.GetPropertySetter(propertyName)));
                }
                else
                {
                    throw new Exception($"Couldn't find path {path}");
                }
            }
        }
    }
}
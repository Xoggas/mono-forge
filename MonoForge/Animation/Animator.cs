using System;
using System.Collections.Generic;
using MonoForge.Ecs;

namespace MonoForge.Animations;

public sealed class Animator : Component
{
    private readonly IAnimatable _target;
    private readonly List<AnimatorBinding> _bindings;
    private float _time;
    private bool _isDirty;

    public Animator(IAnimatable target)
    {
        _target = target;
        _bindings = new List<AnimatorBinding>();
    }

    public bool IsPlaying { get; private set; }
    public float Speed { get; set; } = 1f;
    public AnimationClip? Clip { get; private set; }
    public bool IsLooping { get; set; } = true;

    public float Time
    {
        get => _time;
        set
        {
            _time = value;
            _isDirty = true;
        }
    }

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
        Clip = clip;
        _bindings.Clear();
        CreateBindings();
    }

    public override void Update(GameBase gameBase, float deltaTime)
    {
        base.Update(gameBase, deltaTime);

        if (IsPlaying && Clip is not null)
        {
            UpdateTimeAndSetDirty(deltaTime);
        }

        if (_isDirty)
        {
            UpdateBindingsAndResetDirtyState();
        }
    }

    private void UpdateTimeAndSetDirty(float deltaTime)
    {
        Time += deltaTime * Speed;

        if (IsLooping)
        {
            if (Time < 0f)
            {
                Time = Clip.Duration;
            }

            if (Time >= Clip.Duration)
            {
                Time = 0f;
            }
        }
        else if (Time < 0f || Time >= Clip.Duration)
        {
            Pause();
        }

        _isDirty = true;
    }

    private void UpdateBindingsAndResetDirtyState()
    {
        foreach (AnimatorBinding binding in _bindings)
        {
            binding.Update(Time);
        }

        _isDirty = false;
    }

    private void CreateBindings()
    {
        if (Clip is null)
        {
            return;
        }

        foreach ((var path, Sequence sequence) in Clip.Sequences)
        {
            IAnimatable child = _target;

            foreach (var (start, length, isPropertyName) in Split(path, '/'))
            {
                var name = path.AsSpan().Slice(start, length);

                if (isPropertyName)
                {
                    _bindings.Add(new AnimatorBinding(sequence, _target.GetPropertySetter(name)));
                }
                else
                {
                    child = child.GetChild(name) ?? throw new Exception($"Couldn't find path {path}");
                }
            }
        }
    }

    private static IEnumerable<(int, int, bool)> Split(string str, char symbol)
    {
        var start = 0;

        for (var i = 0; i <= str.Length; i++)
        {
            if (i < str.Length && str[i] != symbol)
            {
                continue;
            }

            var length = i - start;

            yield return (start, length, start + length == str.Length);

            start = i + 1;
        }
    }
}
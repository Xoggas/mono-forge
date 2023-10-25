using System;
using System.Collections.Generic;
using MonoGine.Ecs;

namespace MonoGine.Animations;

public sealed class Animator : Component
{
    public AnimationClip Clip { get; private set; }
    public float Time { get; set; }
    public bool IsPlaying { get; private set; }

    private readonly List<AnimatorBinding> _bindings = new();

    public Animator(IAnimatable target, AnimationClip clip)
    {
        Clip = clip;
        CreateBindingsFor(target);
    }

    public void SetClip(AnimationClip clip)
    {
        Clip = clip;
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
        Time = 0f;
        IsPlaying = false;
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        if (IsPlaying)
        {
            Time += engine.Time.DeltaTime;
        }

        if (Time >= Clip.Duration)
        {
            Time = 0f;
        }

        foreach (AnimatorBinding binding in _bindings)
        {
            binding.Animate(Time);
        }
    }

    private static IAnimatable FindChild(IReadOnlyList<string> names, IAnimatable target)
    {
        IAnimatable? child = target;

        for (var i = 0; i < names.Count - 1; i++)
        {
            child = child.FindChildByName(names[i]);

            if (child is null)
            {
                throw new ArgumentNullException(names[i]);
            }
        }

        return child;
    }

    private void CreateBindingsFor(IAnimatable root)
    {
        foreach ((var name, Sequence sequence) in Clip.Sequences)
        {
            if (name.Contains('/'))
            {
                var names = name.Split('/');

                IAnimatable target = FindChild(names, root);

                CreateBindingFor(target, sequence, names[^1]);
            }
            else
            {
                CreateBindingFor(root, sequence, name);
            }
        }
    }

    private void CreateBindingFor(IAnimatable target, Sequence sequence, string propertyName)
    {
        _bindings.Add(new AnimatorBinding(sequence, value => target.SetProperty(propertyName, value)));
    }
}
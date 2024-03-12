using System;
using MonoForge.Extensions;

namespace MonoForge.Animations;

public readonly struct Keyframe : IComparable<Keyframe>
{
    public readonly float Time;
    public readonly float Value;
    public readonly Ease Ease;

    public Keyframe(float time, float value, Ease ease)
    {
        Time = time;
        Value = value;
        Ease = ease;
    }

    public float Interpolate(Keyframe other, float time)
    {
        var progress = MathExtensions.InverseLerp(Time, other.Time, time);
        return EasingFunctions.GetEasingFunction(other.Ease).Invoke(Value, other.Value, progress);
    }

    public int CompareTo(Keyframe other)
    {
        return Time.CompareTo(other.Time);
    }
}
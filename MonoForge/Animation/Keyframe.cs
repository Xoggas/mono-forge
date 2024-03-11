using System;
using MonoForge.Extensions;

namespace MonoForge.Animations;

public readonly struct Keyframe : IComparable<Keyframe>
{
    public readonly float Time;
    public readonly float Value;
    private readonly Ease _ease;

    public Keyframe(float time, float value, Ease ease)
    {
        Time = time;
        Value = value;
        _ease = ease;
    }

    public float Interpolate(Keyframe other, float time)
    {
        var progress = MathExtensions.InverseLerp(Time, other.Time, time);
        return EasingFunctions.GetEasingFunction(other._ease).Invoke(Value, other.Value, progress);
    }

    public int CompareTo(Keyframe other)
    {
        return Time.CompareTo(other.Time);
    }
}
using System;
using MonoForge.Extensions;

namespace MonoForge.Animations;

[Serializable]
public struct Keyframe : IComparable<Keyframe>
{
    public float Time;
    public float Value;
    public Ease Ease;

    public Keyframe(float time, float value, Ease ease = Ease.Linear)
    {
        Time = time;
        Value = value;
        Ease = ease;
    }

    public Keyframe(float time, bool value, Ease ease = Ease.Linear)
    {
        Time = time;
        Value = value ? 1 : 0;
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
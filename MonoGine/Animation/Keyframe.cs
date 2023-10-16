using System;
using MonoGine.Utilities;

namespace MonoGine.Animations;

[Serializable]
public struct Keyframe : IComparable<Keyframe>
{
    public float Time;
    public float Value;
    public Ease Ease;

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
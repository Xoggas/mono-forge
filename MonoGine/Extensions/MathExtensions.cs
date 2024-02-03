using System;
using Microsoft.Xna.Framework;

namespace MonoGine.Extensions;

public static class MathExtensions
{
    public static bool AreFloatsEqual(float a, float b)
    {
        return MathF.Abs(a - b) < 0.000001f;
    }

    public static float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }

    public static float GetDistance(this Vector2 a, Vector2 b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }
}
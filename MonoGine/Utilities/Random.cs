using Microsoft.Xna.Framework;

namespace MonoGine.Utilities;

public static class Random
{
    public static float Value => System.Random.Shared.NextSingle();
    public static Vector2 InsideUnitCircle => new(Value, Value);

    public static float Range(float min, float max)
    {
        return min + System.Random.Shared.NextSingle() * (max - min);
    }

    public static int Range(int min, int max)
    {
        return System.Random.Shared.Next(min, max);
    }
}
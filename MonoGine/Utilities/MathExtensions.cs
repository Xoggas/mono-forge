namespace MonoGine.Utilities;

public static class MathExtensions
{
    public static float InverseLerp(float a, float b, float v)
    {
        return (v - a) / (b - a);
    }
}
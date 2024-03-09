using Microsoft.Xna.Framework;

namespace MonoGine.Extensions;

public static class PointExtensions
{
    public static Vector3 ToVector3(this Point point, float z = 0f)
    {
        return new Vector3(point.ToVector2(), z);
    }
}
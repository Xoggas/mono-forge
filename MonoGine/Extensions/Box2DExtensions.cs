using Box2DX.Common;
using Microsoft.Xna.Framework;

namespace MonoGine.Extensions;

public static class Box2DExtensions
{
    public static Vector2 ToVector2(this Vec2 vector)
    {
        return new Vector2(vector.X, vector.Y);
    }
}
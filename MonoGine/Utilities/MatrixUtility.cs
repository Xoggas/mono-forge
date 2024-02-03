using Microsoft.Xna.Framework;

namespace MonoGine.Utilities;

internal static class MatrixUtility
{
    private static Matrix CreateSkewMatrix(float skewX, float skewY)
    {
        Matrix matrix = Matrix.Identity;
        matrix.M21 = skewX;
        matrix.M12 = skewY;
        return matrix;
    }

    internal static Matrix CreateTRSSMatrix(Vector2 position, Vector3 rotation, Vector2 scale, Vector2 skew)
    {
        return Matrix.CreateScale(new Vector3(scale, 0f)) *
               CreateSkewMatrix(MathHelper.ToRadians(skew.X), MathHelper.ToRadians(skew.Y)) *
               Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
               Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
               Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) *
               Matrix.CreateTranslation(new Vector3(position, 0f));
    }
}
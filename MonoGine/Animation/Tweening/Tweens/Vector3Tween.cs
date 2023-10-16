using System;
using Microsoft.Xna.Framework;

namespace MonoGine.Animations.Tweening;

public sealed class Vector3Tween : Tween<Vector3>
{
    public Vector3Tween(Vector3 startValue, Vector3 endValue, float duration, Action<Vector3> setter) : base(startValue,
        endValue, duration, setter)
    {
    }

    protected override Vector3 Interpolate(Vector3 startValue, Vector3 endValue, float progress)
    {
        EasingFunctions.Function easingFunction = EasingFunctions.GetEasingFunction(Ease);
        var x = easingFunction.Invoke(startValue.X, endValue.X, progress);
        var y = easingFunction.Invoke(startValue.Y, endValue.Y, progress);
        var z = easingFunction.Invoke(startValue.Z, endValue.Z, progress);
        return new Vector3(x, y, z);
    }
}
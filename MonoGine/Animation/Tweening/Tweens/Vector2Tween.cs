using System;
using Microsoft.Xna.Framework;

namespace MonoGine.Animations.Tweening;

public sealed class Vector2Tween : Tween<Vector2>
{
    public Vector2Tween(Vector2 startValue, Vector2 endValue, float duration, Action<Vector2> setter) : base(startValue,
        endValue, duration, setter)
    {
    }

    protected override Vector2 Interpolate(Vector2 startValue, Vector2 endValue, float progress)
    {
        EasingFunctions.Function easingFunction = EasingFunctions.GetEasingFunction(Ease);
        var x = easingFunction.Invoke(startValue.X, endValue.X, progress);
        var y = easingFunction.Invoke(startValue.Y, endValue.Y, progress);
        return new Vector2(x, y);
    }
}
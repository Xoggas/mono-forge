using System;
using Microsoft.Xna.Framework;
using MonoGine.Ecs;

namespace MonoGine.Animations.Tweening;

public static class TweenHelper
{
    public static FloatTween FromTo(IEntity entity, float startValue, float endValue, float duration,
        Action<float> setter)
    {
        FloatTween tween = new(startValue, endValue, duration, setter);
        entity.AddComponent(tween);
        return tween;
    }

    public static Vector2Tween FromTo(IEntity entity, Vector2 startValue, Vector2 endValue, float duration,
        Action<Vector2> setter)
    {
        Vector2Tween tween = new(startValue, endValue, duration, setter);
        entity.AddComponent(tween);
        return tween;
    }
}
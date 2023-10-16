using Microsoft.Xna.Framework;
using MonoGine.Ecs;
using MonoGine.SceneGraph;

namespace MonoGine.Animations.Tweening;

public static class TweenExtensions
{
    public static Vector2Tween Move(this Transform transform, IEntity entity, Vector2 endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Position, endValue, duration, value => transform.Position = value);
    }

    public static FloatTween MoveX(this Transform transform, IEntity entity, float endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Position.Y, endValue, duration,
            value => transform.Position = new Vector2(value, transform.Position.Y));
    }

    public static FloatTween MoveY(this Transform transform, IEntity entity, float endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Position.Y, endValue, duration,
            value => transform.Position = new Vector2(transform.Position.X, value));
    }

    public static Vector2Tween Resize(this Transform transform, IEntity entity, Vector2 endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Scale, endValue, duration, value => transform.Scale = value);
    }

    public static FloatTween ResizeX(this Transform transform, IEntity entity, float endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Scale.Y, endValue, duration,
            value => transform.Scale = new Vector2(value, transform.Scale.Y));
    }

    public static FloatTween ResizeY(this Transform transform, IEntity entity, float endValue, float duration)
    {
        return TweenHelper.FromTo(entity, transform.Scale.Y, endValue, duration,
            value => transform.Scale = new Vector2(transform.Scale.X, value));
    }
}
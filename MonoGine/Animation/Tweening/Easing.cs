using System;
using Microsoft.Xna.Framework;

namespace MonoGine.Animations.Tweening;

public static class EasingFunctions
{
    public delegate float Function(float s, float e, float v);

    public static Function GetEasingFunction(Ease easingFunction)
    {
        return easingFunction switch
        {
            Ease.InQuad => InQuad,
            Ease.OutQuad => OutQuad,
            Ease.InOutQuad => InOutQuad,
            Ease.InCubic => InCubic,
            Ease.OutCubic => OutCubic,
            Ease.InOutCubic => InOutCubic,
            Ease.InQuart => InQuart,
            Ease.OutQuart => OutQuart,
            Ease.InOutQuart => InOutQuart,
            Ease.InQuint => InQuint,
            Ease.OutQuint => OutQuint,
            Ease.InOutQuint => InOutQuint,
            Ease.InSine => InSine,
            Ease.OutSine => OutSine,
            Ease.InOutSine => InOutSine,
            Ease.InExpo => InExpo,
            Ease.OutExpo => OutExpo,
            Ease.InOutExpo => InOutExpo,
            Ease.InCirc => InCirc,
            Ease.OutCirc => OutCirc,
            Ease.InOutCirc => InOutCirc,
            Ease.Linear => Linear,
            Ease.Spring => Spring,
            Ease.InBounce => InBounce,
            Ease.OutBounce => OutBounce,
            Ease.InOutBounce => InOutBounce,
            Ease.InBack => InBack,
            Ease.OutBack => OutBack,
            Ease.InOutBack => InOutBack,
            Ease.InElastic => InElastic,
            Ease.OutElastic => OutElastic,
            Ease.InOutElastic => InOutElastic,
            _ => Linear
        };
    }

    private static float Linear(float start, float end, float value)
    {
        return MathHelper.Lerp(start, end, value);
    }

    private static float Spring(float start, float end, float value)
    {
        value = MathHelper.Clamp(value, 0f, 1f);
        value = (MathF.Sin(value * MathF.PI * (0.2f + 2.5f * value * value * value)) * MathF.Pow(1f - value, 2.2f) +
                 value) * (1f + 1.2f * (1f - value));
        return start + (end - start) * value;
    }

    private static float InQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    private static float OutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    private static float InOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    private static float InCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    private static float OutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    private static float InOutCubic(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value + 2) + start;
    }

    private static float InQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    private static float OutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    private static float InOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    private static float InQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    private static float OutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    private static float InOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value * value + start;
        value -= 2;
        return end * 0.5f * (value * value * value * value * value + 2) + start;
    }

    private static float InSine(float start, float end, float value)
    {
        end -= start;
        return -end * MathF.Cos(value * (MathF.PI * 0.5f)) + end + start;
    }

    private static float OutSine(float start, float end, float value)
    {
        end -= start;
        return end * MathF.Sin(value * (MathF.PI * 0.5f)) + start;
    }

    private static float InOutSine(float start, float end, float value)
    {
        end -= start;
        return -end * 0.5f * (MathF.Cos(MathF.PI * value) - 1) + start;
    }

    private static float InExpo(float start, float end, float value)
    {
        end -= start;
        return end * MathF.Pow(2, 10 * (value - 1)) + start;
    }

    private static float OutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-MathF.Pow(2, -10 * value) + 1) + start;
    }

    private static float InOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * MathF.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end * 0.5f * (-MathF.Pow(2, -10 * value) + 2) + start;
    }

    private static float InCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (MathF.Sqrt(1 - value * value) - 1) + start;
    }

    private static float OutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * MathF.Sqrt(1 - value * value) + start;
    }

    private static float InOutCirc(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return -end * 0.5f * (MathF.Sqrt(1 - value * value) - 1) + start;
        value -= 2;
        return end * 0.5f * (MathF.Sqrt(1 - value * value) + 1) + start;
    }

    private static float InBounce(float start, float end, float value)
    {
        end -= start;
        var d = 1f;
        return end - OutBounce(0, end, d - value) + start;
    }

    private static float OutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;

        switch (value)
        {
            case < 1 / 2.75f:
                return end * (7.5625f * value * value) + start;
            case < 2 / 2.75f:
                value -= 1.5f / 2.75f;
                return end * (7.5625f * value * value + .75f) + start;
        }

        if (value < 2.5 / 2.75)
        {
            value -= 2.25f / 2.75f;
            return end * (7.5625f * value * value + .9375f) + start;
        }

        value -= 2.625f / 2.75f;

        return end * (7.5625f * value * value + .984375f) + start;
    }

    private static float InOutBounce(float start, float end, float value)
    {
        end -= start;
        var d = 1f;

        if (value < d * 0.5f)
            return InBounce(0, end, value * 2) * 0.5f + start;

        return OutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }

    private static float InBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        var s = 1.70158f;
        return end * value * value * ((s + 1) * value - s) + start;
    }

    private static float OutBack(float start, float end, float value)
    {
        var s = 1.70158f;
        end -= start;
        value = value - 1;
        return end * (value * value * ((s + 1) * value + s) + 1) + start;
    }

    private static float InOutBack(float start, float end, float value)
    {
        var s = 1.70158f;
        end -= start;
        value /= .5f;
        if (value < 1)
        {
            s *= 1.525f;
            return end * 0.5f * (value * value * ((s + 1) * value - s)) + start;
        }

        value -= 2;
        s *= 1.525f;
        return end * 0.5f * (value * value * ((s + 1) * value + s) + 2) + start;
    }

    private static float InElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < MathF.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * MathF.PI) * MathF.Asin(end / a);
        }

        return -(a * MathF.Pow(2, 10 * (value -= 1)) * MathF.Sin((value * d - s) * (2 * MathF.PI) / p)) + start;
    }

    private static float OutElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if (Math.Abs((value /= d) - 1) < 0.000001f) return start + end;

        if (a == 0f || a < MathF.Abs(end))
        {
            a = end;
            s = p * 0.25f;
        }
        else
        {
            s = p / (2 * MathF.PI) * MathF.Asin(end / a);
        }

        return a * MathF.Pow(2, -10 * value) * MathF.Sin((value * d - s) * (2 * MathF.PI) / p) + end + start;
    }

    private static float InOutElastic(float start, float end, float value)
    {
        end -= start;

        var d = 1f;
        var p = d * .3f;
        float s;
        float a = 0;

        if (value == 0) return start;

        if (Math.Abs((value /= d * 0.5f) - 2) < 0.000001f) return start + end;

        if (a == 0f || a < MathF.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * MathF.PI) * MathF.Asin(end / a);
        }

        if (value < 1)
            return -0.5f * (a * MathF.Pow(2, 10 * (value -= 1)) * MathF.Sin((value * d - s) * (2 * MathF.PI) / p)) +
                   start;
        return a * MathF.Pow(2, -10 * (value -= 1)) * MathF.Sin((value * d - s) * (2 * MathF.PI) / p) * 0.5f + end +
               start;
    }
}
using System.Collections.Generic;
using System.Linq;

namespace MonoForge.Animations;

public sealed class Sequence
{
    public IEnumerable<Keyframe> Keyframes => _keyframes;
    public float Duration => _keyframes.Length != 0 ? _keyframes[^1].Time : 0f;

    private readonly Keyframe[] _keyframes;

    public Sequence(IEnumerable<Keyframe> keyframes)
    {
        _keyframes = keyframes.OrderBy(x => x.Time).ToArray();
    }

    public float Evaluate(float time)
    {
        switch (_keyframes.Length)
        {
            case 0:
                return 0f;
            case 1:
                return _keyframes[0].Value;
            case 2:
                return _keyframes[0].Interpolate(_keyframes[1], time);
        }

        if (time <= _keyframes[0].Time)
        {
            return _keyframes[0].Value;
        }

        if (time >= _keyframes[^1].Time)
        {
            return _keyframes[^1].Value;
        }

        var keyframeIndex = FindKeyframe(time);

        return _keyframes[keyframeIndex].Interpolate(_keyframes[keyframeIndex + 1], time);
    }

    private int FindKeyframe(float time)
    {
        var low = 0;
        var high = _keyframes.Length - 1;

        while (low <= high)
        {
            var middle = (low + high) / 2;

            if (time > _keyframes[middle].Time)
            {
                low = middle + 1;
            }
            else if (time < _keyframes[middle].Time)
            {
                high = middle - 1;
            }
            else
            {
                return middle;
            }
        }

        return low - 1;
    }
}
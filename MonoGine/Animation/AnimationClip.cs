using System;
using System.Collections.Generic;

namespace MonoGine.Animations;

[Serializable]
public class AnimationClip
{
    private Dictionary<string, Sequence> _sequences = new();
}
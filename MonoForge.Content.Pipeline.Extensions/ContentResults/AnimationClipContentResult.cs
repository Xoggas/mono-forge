using System;
using System.Collections.Generic;

namespace MonoGine.Content.Pipeline.Extensions;

[Serializable]
public class AnimationClipContentResult
{
    public Dictionary<string, Sequence> Sequences = new();
}

[Serializable]
public class Sequence
{
    public Keyframe[] Keyframes = Array.Empty<Keyframe>();
}

[Serializable]
public class Keyframe
{
    public float Time { get; set; }
    public float Value { get; set; }
    public int Ease { get; set; }
}
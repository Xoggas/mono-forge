using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using MonoForge.Animations;

namespace MonoGine.Content.Pipeline.Extensions.Writers;

[ContentTypeWriter]
public sealed class AnimationClipWriter : ContentTypeWriter<AnimationClip>
{
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return $"";
    }

    protected override void Write(ContentWriter output, AnimationClip value)
    {
        output.Write(value.Sequences.Count);

        foreach ((var name, Sequence sequence) in value.Sequences)
        {
            output.Write(name);
            output.Write(sequence.Keyframes.Count());

            foreach (Keyframe keyframe in sequence.Keyframes)
            {
                output.Write(keyframe.Time);
                output.Write(keyframe.Value);
                output.Write((int)keyframe.Ease);
            }
        }
    }
}
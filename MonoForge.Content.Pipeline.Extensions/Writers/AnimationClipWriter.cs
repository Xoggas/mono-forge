using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace MonoGine.Content.Pipeline.Extensions.Writers;

[ContentTypeWriter]
public sealed class AnimationClipWriter : ContentTypeWriter<AnimationClipContentResult>
{
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "";
    }

    protected override void Write(ContentWriter output, AnimationClipContentResult value)
    {
        output.Write(value.Sequences.Count);

        foreach ((var name, Sequence sequence) in value.Sequences)
        {
            output.Write(name);
            output.Write(sequence.Keyframes.Length);

            foreach (Keyframe keyframe in sequence.Keyframes)
            {
                output.Write(keyframe.Time);
                output.Write(keyframe.Value);
                output.Write(keyframe.Ease);
            }
        }
    }
}
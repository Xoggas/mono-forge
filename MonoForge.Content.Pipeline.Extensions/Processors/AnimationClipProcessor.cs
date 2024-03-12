using Microsoft.Xna.Framework.Content.Pipeline;
using MonoForge.Animations;

namespace MonoGine.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Animation Clip Processor")]
public sealed class AnimationClipProcessor : ContentProcessor<AnimationClip, AnimationClip>
{
    public override AnimationClip Process(AnimationClip input, ContentProcessorContext context)
    {
        return input;
    }
}
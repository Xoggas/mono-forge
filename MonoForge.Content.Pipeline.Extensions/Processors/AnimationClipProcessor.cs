using Microsoft.Xna.Framework.Content.Pipeline;

namespace MonoGine.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Animation Clip Processor")]
public sealed class AnimationClipProcessor : ContentProcessor<AnimationClipContentResult, AnimationClipContentResult>
{
    public override AnimationClipContentResult Process(AnimationClipContentResult input,
        ContentProcessorContext context)
    {
        return input;
    }
}
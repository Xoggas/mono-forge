﻿using Microsoft.Xna.Framework.Content.Pipeline;
using TInput = System.String;
using TOutput = System.String;

namespace MonoForge.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Animation Clip Processor")]
internal class AnimationClipProcessor : ContentProcessor<TInput, TOutput>
{
    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
        return default;
    }
}
using Microsoft.Xna.Framework.Content.Pipeline;
using TInput = System.String;
using TOutput = System.String;

namespace MonoGine.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Processor1")]
internal class Processor1 : ContentProcessor<TInput, TOutput>
{
    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
        return default;
    }
}
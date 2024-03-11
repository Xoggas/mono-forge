using Microsoft.Xna.Framework.Content.Pipeline;
using MonoForge.Animations;

namespace MonoForge.Content.Pipeline.Extensions;

[ContentImporter(".clip", DisplayName = "Animation Clip Importer", DefaultProcessor = "Animation Clip Processor")]
public class AnimationClipImporter : ContentImporter<AnimationClip>
{
    public override AnimationClip Import(string filename, ContentImporterContext context)
    {
        return default;
    }
}
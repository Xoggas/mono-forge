using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoForge.Animations;
using Newtonsoft.Json;

namespace MonoGine.Content.Pipeline.Extensions.Importers;

[ContentImporter(".json", DisplayName = "Animation Clip Importer", DefaultProcessor = nameof(AnimationClipProcessor))]
public sealed class AnimationClipImporter : ContentImporter<AnimationClip>
{
    public override AnimationClip Import(string filename, ContentImporterContext context)
    {
        var json = File.ReadAllText(filename);
        return JsonConvert.DeserializeObject<AnimationClip>(json);
    }
}
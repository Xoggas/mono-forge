using System.IO;
using MonoGine.Animations;
using Newtonsoft.Json;

namespace MonoGine.AssetLoading;

internal sealed class AnimationClipProcessor : IAssetReader<AnimationClip>, IAssetWriter<AnimationClip>
{
    public AnimationClip Read(IEngine engine, string localPath)
    {
        var absolutePath = PathUtility.GetAbsoluteAssetPath(localPath);
        var jsonText = File.ReadAllText(absolutePath);
        return JsonConvert.DeserializeObject<AnimationClip>(jsonText) ?? throw new JsonReaderException();
    }

    public void Write(IEngine engine, string localPath, AnimationClip resource)
    {
        var absolutePath = PathUtility.GetAbsoluteAssetPath(localPath);
        var jsonText = JsonConvert.SerializeObject(resource);
        File.WriteAllText(absolutePath, jsonText);
    }
}
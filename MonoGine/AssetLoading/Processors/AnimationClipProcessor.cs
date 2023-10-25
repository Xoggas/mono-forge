using System.IO;
using MonoGine.Animations;
using Newtonsoft.Json;

namespace MonoGine.AssetLoading;

public sealed class AnimationClipProcessor : IAssetReader<AnimationClip>, IAssetWriter<AnimationClip>
{
    public AnimationClip Read(IEngine engine, string path)
    {
        var absolutePath = PathUtils.GetAbsolutePath(path);
        var jsonText = File.ReadAllText(absolutePath);
        return JsonConvert.DeserializeObject<AnimationClip>(jsonText) ?? throw new JsonReaderException();
    }

    public void Write(IEngine engine, string path, AnimationClip resource)
    {
        var absolutePath = PathUtils.GetAbsolutePath(path);
        var jsonText = JsonConvert.SerializeObject(resource);
        File.WriteAllText(absolutePath, jsonText);
    }
}
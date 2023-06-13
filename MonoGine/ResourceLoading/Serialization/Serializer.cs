using System.Collections.Generic;
using System.Linq;

namespace MonoGine.ResourceLoading;

internal sealed class Serializer
{
    private string _assetsPath;
    private Dictionary<string, Metadata> _serializedAssets;

    internal Serializer()
    {
        _assetsPath = PathUtils.GetOrCreateAssetsFolder();
        _processors = new Processors();
        _serializedAssets = new Dictionary<string, Metadata>();
    }

    internal Processors _processors { get; private set; }

    internal Metadata GetMetadata(string path)
    {
        return _serializedAssets[path];
    }

    internal void SerializeAll()
    {
        IEnumerable<string> paths = GetAssetPaths();

        foreach (var path in paths)
        {
            Serialize(path);
        }
    }

    internal void Serialize(string assetPath)
    {
        string metadataPath = GetMetadataPath(assetPath);


    }

    private void Register(string absolutePath, Metadata metadata)
    {
        string relativePath = PathUtils.GetLocalPath(_assetsPath, absolutePath);

        _serializedAssets.Add(relativePath, metadata);
    }

    private IEnumerable<string> GetAssetPaths()
    {
        return PathUtils.GetFiles(_assetsPath).Where(path => path.EndsWith(".meta"));
    }

    private string GetMetadataPath(string assetPath)
    {
        return PathUtils.ChangeExtension(assetPath, "meta");
    }
}

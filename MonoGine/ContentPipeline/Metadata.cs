using System.IO;

namespace MonoGine.ContentPipeline;

public class Metadata : Object
{
    private string _metadataPath;
    private JSONNode _data;

    public Metadata(string metaDataPath)
    {
        _metadataPath = metaDataPath + ".meta";
        _data = JSON.Parse(File.ReadAllText(_metadataPath));
    }

    internal Metadata(string metadataPath, JSONNode node)
    {
        _metadataPath = metadataPath;
        _data = node;
    }

    public JSONNode this[string name]
    {
        get => _data[name];
        set => _data[name] = value;
    }

    public void Save()
    {
        File.WriteAllText(_metadataPath, _data.ToString());
    }

    public override void Dispose()
    {
        _data.Clear();
    }
}

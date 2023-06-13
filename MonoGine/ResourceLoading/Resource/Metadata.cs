using System.IO;
using System.Text.Json.Nodes;

namespace MonoGine.ResourceLoading;

public class Metadata : Object
{
    private JsonObject _metadata;

    internal Metadata(JsonObject metadata)
    {
        _metadata = metadata;
    }

    internal Metadata()
    {
        _metadata = new JsonObject();
    }

    internal Metadata(string metadataPath)
    {
        _metadata = JsonNode.Parse(File.ReadAllText(metadataPath)).AsObject();
    }

    public string Name => _metadata["name"].GetValue<string>();
    public string Path => _metadata["path"].GetValue<string>();
    public JsonNode this[string path] { get => _metadata[path]; set => _metadata[path] = value; }

    public override string ToString()
    {
        return _metadata.ToJsonString();
    }

    public override void Dispose()
    {
        _metadata = null;
    }
}

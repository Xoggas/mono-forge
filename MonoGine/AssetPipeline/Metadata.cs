using System.IO;

namespace MonoGine.AssetPipeline;

public class Metadata : Object
{
    private JSONNode _data;

    public Metadata(string metaDataPath)
    {
        metaDataPath = metaDataPath + ".meta";

        using(FileStream stream = File.OpenRead(metaDataPath))
        {
            _data = JSON.Parse(metaDataPath);
        }
    }

    public JSONNode this[string name] => _data[name];  

    public override void Dispose()
    {
        _data.Clear();
    }
}

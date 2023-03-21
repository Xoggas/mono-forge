using System.Collections.Generic;

namespace MonoGine.AssetPipeline;

public sealed class Resources : System
{
    private Serializer _serializer;
    private Dictionary<string, Asset> _assets;

    public Resources(string assetFolderPath)
    {
        _serializer = new Serializer(assetFolderPath);
    }

    public override void Initialize()
    {
        int a = 0;
        while (a++ < 10) ;

    }

    public override void Update()
    {
        
    }

    public override void Dispose()
    {
        
    }
}

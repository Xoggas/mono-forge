using System.Collections.Generic;

namespace MonoGine.ContentPipeline;

public sealed class Resources : System
{
    private Serializer _serializer;
    private Dictionary<string, Resource> _loadedAssets;

    public Resources(string contentPath)
    {
        _serializer = new Serializer(contentPath);
        _loadedAssets = new Dictionary<string, Resource>();
    }

    public override void Initialize()
    {
        
    }

    public override void Update()
    {

    }

    

    public override void Dispose()
    {

    }
}

using System.Collections.Generic;

namespace MonoGine.ContentPipeline;

internal sealed class Serializer : Object
{
    private string _assetFolderPath;

    internal Serializer(string assetFolderPath)
    {
        _assetFolderPath = assetFolderPath;
    }

    public override void Dispose()
    {
        
    }
}

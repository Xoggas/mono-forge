using System.Collections.Generic;

namespace MonoGine.ResourceLoading;

internal sealed class Cache : Object
{
    private Dictionary<string, Resource> _assets;

    internal Cache()
    {
        _assets = new Dictionary<string, Resource>();
    }

    public override void Dispose()
    {
        _assets.Clear();
    }

    internal void Add(string path, Resource asset)
    {
        
    }

    internal void Clear()
    {
        _assets.Clear();
    }

    internal void Remove(string path)
    {

    }
}

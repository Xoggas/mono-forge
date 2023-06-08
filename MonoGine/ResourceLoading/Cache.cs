using System.Collections.Generic;

namespace MonoGine.ResourceLoading;

internal sealed class Cache : Object
{
    private Dictionary<string, Asset> _assets;

    internal Cache()
    {
        _assets = new Dictionary<string, Asset>();
    }

    public override void Dispose()
    {
        _assets.Clear();
    }

    internal void Add(string path, Asset asset)
    {
        
    }

    internal void Clear()
    {
        _assets.Clear();
    }

    internal void Remove(string path)
    {

    }

    private string FormatPath(string path)
    {
        if (path.StartsWith('/'))
        {
            path = path[1..];
        }

        if(path.EndsWith('/'))
        {
            path = path[..^1];
        }

        if (path.Contains('.'))
        {
            path = path.Split('.')[0];
        }

        return path;
    }
}

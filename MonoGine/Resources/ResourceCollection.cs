using System.Collections.Generic;

namespace MonoGine.Resources;

internal class ResourceCollection : IObject
{
    private GenericDictionary<string> _resources;

    internal ResourceCollection()
    {
        _resources = new GenericDictionary<string>();
    }

    public void Dispose()
    {
        _resources.Clear();
    }

    internal bool TryAdd<T>(string key, T resource) where T : notnull
    {
        return _resources.TryAdd(FormatKey(key), resource);
    }

    internal bool TryGet<T>(string key, out T? asset) where T : notnull
    {
        return _resources.TryGet(FormatKey(key), out asset);
    }

    private string FormatKey(string key)
    {
        key = key.Replace('\\', '/');

        if (key.StartsWith('/'))
        {
            key = key[1..];
        }

        return key;
    }
}

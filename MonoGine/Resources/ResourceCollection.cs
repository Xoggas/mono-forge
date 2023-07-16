using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MonoGine.Resources;

internal class ResourceCollection : IObject
{
    private readonly GenericDictionary<string> _resources;

    internal ResourceCollection()
    {
        _resources = new GenericDictionary<string>();
    }

    private static string FormatKey(string key)
    {
        key = key.Replace('\\', '/');

        if (key.StartsWith('/'))
        {
            key = key[1..];
        }

        return key;
    }

    public void Dispose()
    {
        foreach (var resource in _resources)
        {
            (resource.Value as IDisposable)?.Dispose();
        }

        _resources.Clear();
    }

    internal bool TryAdd<T>(string key, T resource) where T : notnull
    {
        return _resources.TryAdd(FormatKey(key), resource);
    }

    internal bool TryGet<T>(string key, [MaybeNullWhen(false)] out T asset) where T : notnull
    {
        return _resources.TryGet(FormatKey(key), out asset);
    }

    internal bool TryRemove(string key)
    {
        return _resources.Remove(FormatKey(key));
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MonoGine.ResourceLoading;

public static class DictionaryExtensions
{
    internal static void Add<T>(this Dictionary<Type, IResourceProcessor> processors, IResourceProcessor resourceProcessor)
    {
        processors.Add(typeof(T), resourceProcessor);
    }

    internal static bool TryGetReader<T>(this Dictionary<Type, IResourceProcessor> processors,
        [NotNullWhen(true)] out IResourceReader<T>? reader) where T : class, IResource
    {
        if (processors.TryGetValue(typeof(T), out IResourceProcessor? processor) &&
            processor is IResourceReader<T> assetReader)
        {
            reader = assetReader;
            return true;
        }

        reader = default;
        return false;
    }

    internal static bool TryGeWriter<T>(this Dictionary<Type, IResourceProcessor> processors,
        [NotNullWhen(true)] out IResourceWriter<T>? writer) where T : class, IResource
    {
        if (processors.TryGetValue(typeof(T), out IResourceProcessor? processor) &&
            processor is IResourceWriter<T> assetWriter)
        {
            writer = assetWriter;
            return true;
        }

        writer = default;
        return false;
    }
}
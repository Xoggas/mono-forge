using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MonoGine.Resources;

internal class ProcessorCollection : IObject
{
    private Dictionary<Type, IProcessor> _processors;

    internal ProcessorCollection()
    {
        _processors = new Dictionary<Type, IProcessor>();
    }

    public void Dispose()
    {
        _processors.Clear();
    }

    internal bool TryAdd<T>(IProcessor processor) where T : class
    {
        return _processors.TryAdd(typeof(T), processor);
    }

    internal bool TryGet<T>([MaybeNullWhen(false)] out IProcessor processor) where T : notnull
    {
        return _processors.TryGetValue(typeof(T), out processor);
    }
}

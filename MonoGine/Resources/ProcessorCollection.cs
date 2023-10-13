using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MonoGine.ResourceLoading;

internal class ProcessorCollection : IObject
{
    private readonly GenericDictionary<Type> _processors;

    internal ProcessorCollection()
    {
        _processors = new GenericDictionary<Type>();
    }

    public void Dispose()
    {
        _processors.Clear();
    }

    internal bool TryAdd<T>(IProcessor<T> processor) where T : class
    {
        return _processors.TryAdd(typeof(T), processor);
    }

    internal bool TryGet<T>([MaybeNullWhen(false)] out IProcessor<T> processor) where T : class
    {
        return _processors.TryGet(typeof(T), out processor);
    }
}

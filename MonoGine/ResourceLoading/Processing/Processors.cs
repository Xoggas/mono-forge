using System;
using System.Collections.Generic;

namespace MonoGine.ResourceLoading;

internal class Processors
{
    private Dictionary<string, Processor> _processors;

    internal Processors()
    {
        _processors = new Dictionary<string, Processor>();
    }

    internal bool Support(string extension)
    {
        return _processors.ContainsKey(extension);
    }

    internal void Register<T>(string[] extensions) where T : Processor
    {
        T processor = Activator.CreateInstance(typeof(T)) as T;

        foreach (var extension in extensions)
        {
            if (!_processors.TryAdd(extension, processor))
            {
                throw new ArgumentException($"Duplicate extension: {extension}!");
            }
        }
    }

    internal bool TryGetProcessor(string extension, out Processor processor)
    {
        return _processors.TryGetValue(extension, out processor);   
    }

    internal bool TryGetProcessor<T>(string extension, out T processor) where T : Processor
    {
        if (_processors.ContainsKey(extension))
        {
            processor = _processors[extension] as T;
            return true;
        }
        else
        {
            processor = null;
            return false;
        }
    }
}
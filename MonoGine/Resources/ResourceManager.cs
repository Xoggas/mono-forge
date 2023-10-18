using System;
using System.Collections.Generic;
using MonoGine.Audio;
using MonoGine.Rendering;

namespace MonoGine.ResourceLoading;

/// <summary>
/// Represents a resource manager responsible for loading, saving, and managing resources.
/// </summary>
public sealed class ResourceManager : IResourceManager
{
    private readonly IEngine _engine;
    private readonly ResourceCollection _resources;
    private readonly Dictionary<Type, IResourceProcessor> _processors = new();

    /// <summary>
    /// Initializes a new instance of the ResourceManager class.
    /// </summary>
    internal ResourceManager(IEngine engine)
    {
        _engine = engine;
        _resources = new ResourceCollection();
    }

    /// <summary>
    /// Initializes the resource manager.
    /// </summary>
    /// <param name="engine"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Initialize(IEngine engine)
    {
        RegisterProcessor<Sprite>(new SpriteResourceProcessor());
        RegisterProcessor<Shader>(new ShaderResourceProcessor());
        RegisterProcessor<AudioClip>(new AudioClipResourceProcessor());
    }

    /// <summary>
    /// Registers a processor for handling the loading and saving of resources of type T.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="resourceProcessor">The processor to register.</param>
    public void RegisterProcessor<T>(IResourceProcessor resourceProcessor) where T : class, IResource
    {
        _processors.Add<T>(resourceProcessor);
    }

    /// <summary>
    /// Loads a resource of type T from the specified path.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="path">The path to the resource.</param>
    /// <returns>The loaded resource, or null if it failed to load.</returns>
    public T LoadFromFile<T>(string path) where T : class, IResource
    {
        if (_engine == null)
        {
            throw new InvalidOperationException("The engine is null!");
        }

        if (_resources.TryGet<T>(path, out T? cachedAsset))
        {
            return cachedAsset;
        }

        PathUtils.ValidatePath(path);

        if (_processors.TryGetReader<T>(out var reader))
        {
            T result = reader.Read(_engine, path);

            _resources.TryAdd(path, result);

            return result;
        }

        throw new InvalidOperationException($"Can't process file of type {typeof(T)}");
    }

    /// <summary>
    /// Saves a resource of type T to the specified path.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="path">The path to save the resource to.</param>
    /// <param name="resource">The resource to save.</param>
    public void SaveToFile<T>(string path, T resource) where T : class, IResource
    {
        if (_processors.TryGeWriter<T>(out var writer))
        {
            writer.Write(_engine, path, resource);
        }
    }

    /// <summary>
    /// Unloads the resource at the specified path.
    /// </summary>
    /// <param name="path">The path of the resource to unload.</param>
    public void Unload(string path)
    {
        if (_resources.TryGet<IDisposable>(path, out IDisposable? cachedAsset))
        {
            cachedAsset.Dispose();
        }

        _resources.TryRemove(path);
    }

    /// <summary>
    /// Disposes the resource manager and releases any resources it holds.
    /// </summary>
    public void Dispose()
    {
        _resources.Dispose();
    }
}
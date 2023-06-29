using System;
using MonoGine.Resources;

namespace MonoGine.ResourceLoading;

/// <summary>
/// Represents a resource manager responsible for loading, saving, and managing resources.
/// </summary>
public sealed class ResourceManager : IResourceManager
{
    private readonly IEngine _engine;
    private readonly ResourceCollection _resources;
    private readonly ProcessorCollection _processors;

    /// <summary>
    /// Initializes a new instance of the ResourceManager class.
    /// </summary>
    internal ResourceManager(IEngine engine)
    {
        _engine = engine;
        _resources = new ResourceCollection();
        _processors = new ProcessorCollection();
    }

    /// <summary>
    /// Registers a processor for handling the loading and saving of resources of type T.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="processor">The processor to register.</param>
    public void RegisterProcessor<T>(IProcessor processor) where T : class
    {
        _processors.TryAdd<T>(processor);
    }

    /// <summary>
    /// Loads a resource of type T from the specified path.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="path">The path to the resource.</param>
    /// <returns>The loaded resource, or null if it failed to load.</returns>
    public T Load<T>(string path) where T : class
    {
        if (_engine == null)
        {
            throw new InvalidOperationException("The engine is null!");
        }

        if (_resources.TryGet<T>(path, out var cachedAsset))
        {
            return cachedAsset;
        }

        PathUtils.ValidatePath(path);

        if (_processors.TryGet<T>(out var processor))
        {
            var result = processor.Load<T>(_engine, path);

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
    public void Save<T>(string path, T resource) where T : class
    {
        if (_processors.TryGet<T>(out var processor))
        {
            processor.Save(_engine, path, resource);
        }
    }

    /// <summary>
    /// Unloads the resource at the specified path.
    /// </summary>
    /// <param name="path">The path of the resource to unload.</param>
    public void Unload(string path)
    {
    }

    /// <summary>
    /// Disposes the resource manager and releases any resources it holds.
    /// </summary>
    public void Dispose()
    {
        _processors.Dispose();
        _resources.Dispose();
    }
}
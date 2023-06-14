using MonoGine.Resources;
using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed class ResourceManager : IResourceManager
{
    private Engine? _engine;
    private ResourceCollection _resources;
    private ProcessorCollection _processors;

    internal ResourceManager()
    {
        _resources = new ResourceCollection();
        _processors = new ProcessorCollection();
    }

    public void RegisterProcessor<T>(IProcessor processor) where T : class
    {
        _processors.TryAdd<T>(processor);
    }

    public void Initialize(Engine engine)
    {
        _engine = engine;
    }

    public T? Load<T>(string path) where T : class
    {
        if (_engine == null)
        {
            return null;
        }

        if (_resources.TryGet(path, out T? cachedAsset))
        {
            return cachedAsset;
        }

        if (_processors.TryGet<T>(out IProcessor? processor))
        {
            var result = processor.Load<T>(_engine, path);

            if (result != null)
            {
                _resources.TryAdd(path, result);
            }

            return result;
        }

        return null;
    }

    public async Task<T?> LoadAsync<T>(string path) where T : class
    {
        if (_engine == null)
        {
            return await Task.FromResult<T?>(null);
        }

        if (_resources.TryGet(path, out T? cachedAsset))
        {
            return cachedAsset;
        }

        if (_processors.TryGet<T>(out IProcessor? processor))
        {
            var result = await processor.LoadAsync<T>(_engine, path);

            if (result != null)
            {
                _resources.TryAdd(path, result);
            }

            return result;
        }

        return null;
    }

    public void Save<T>(string path, T? resource) where T : class
    {
        if (_engine == null || resource == null)
        {
            return;
        }

        if (_processors.TryGet<T>(out IProcessor? processor))
        {
            processor?.Save(_engine, path, resource);
        }
    }

    public async Task SaveAsync<T>(string path, T? resource) where T : class
    {
        if (_engine == null || resource == null)
        {
            return;
        }

        if (_processors.TryGet<T>(out IProcessor? processor))
        {
            await processor.SaveAsync(_engine, path, resource);
        }
    }

    public void Dispose()
    {
        _engine = null;
        _processors.Dispose();
        _resources.Dispose();
    }
}

using MonoGine.Resources;
using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed class ResourceManager : IResourceManager
{
    public void RegisterProcessor<T>() where T : class, IProcessor
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(Engine engine)
    {
        throw new System.NotImplementedException();
    }

    public T Load<T>(string path)
    {
        throw new System.NotImplementedException();
    }

    public Task<T> LoadAsync<T>(string path)
    {
        throw new System.NotImplementedException();
    }

    public void Save<T>(string path, T resource)
    {
        throw new System.NotImplementedException();
    }

    public Task SaveAsync<T>(string path, T resource)
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}

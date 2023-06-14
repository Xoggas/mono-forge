using System.Threading.Tasks;

namespace MonoGine.Resources;

public interface IResourceManager : IObject, IInitializable
{
    public void RegisterProcessor<T>() where T : class, IProcessor;
    public T Load<T>(string path);
    public Task<T> LoadAsync<T>(string path);
    public void Save<T>(string path, T resource);
    public Task SaveAsync<T>(string path, T resource);
}

using System.Threading.Tasks;

namespace MonoGine.Resources;

public interface IResourceManager : IObject, IInitializable
{
    public void RegisterProcessor<T>(IProcessor processor) where T : class;
    public T? Load<T>(string path) where T : class;
    public Task<T?> LoadAsync<T>(string path) where T : class;
    public void Save<T>(string path, T? resource) where T : class;
    public Task SaveAsync<T>(string path, T? resource) where T : class;
}

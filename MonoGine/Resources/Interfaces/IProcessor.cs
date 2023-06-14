using System.Threading.Tasks;

namespace MonoGine.Resources;

public interface IProcessor
{
    public T Load<T>(string path);
    public void Save<T>(string path, T resource);
    public Task<T> LoadAsync<T>(string path);
    public Task SaveAsync<T>(string path, T resource);
}

using System.IO;
using System.Threading.Tasks;

namespace MonoGine.Resources;

public interface IProcessor
{
    public T? Load<T>(Engine engine, string path) where T : class;
    public Task<T?> LoadAsync<T>(Engine engine, string path) where T : class;
    public void Save<T>(Engine engine, string path, T? resource) where T : class;
    public Task SaveAsync<T>(Engine engine, string path, T? resource) where T : class;
}

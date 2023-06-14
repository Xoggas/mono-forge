using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace MonoGine.Resources;

internal sealed class Texture2DProcessor : IProcessor
{
    public T? Load<T>(Engine engine, string path) where T : class
    {
        return Texture2D.FromFile(engine.GraphicsDevice, Path.Combine(Directory.GetCurrentDirectory(), "Assets", path)) as T;
    }

    public async Task<T?> LoadAsync<T>(Engine engine, string path) where T : class
    {
        return await Task.Run(() => Load<T>(engine, path));
    }

    public void Save<T>(Engine engine, string path, T? resource) where T : class
    {
        throw new System.NotImplementedException();
    }

    public Task SaveAsync<T>(Engine engine, string path, T? resource) where T : class
    {
        throw new System.NotImplementedException();
    }
}

using Microsoft.Xna.Framework.Graphics;
using MonoGine.Graphics;
using System.IO;
using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed partial class Resources : ISystem
{
    private static string s_assetsPath;
    private static Processors s_processors;
    private static Cache s_cache;

    internal Resources()
    {
        s_assetsPath = PathUtils.GetAssetsPath();
        s_processors = new Processors();
        s_cache = new Cache();
    }

    public static void RegisterProcessor<T, U>() where T : class, IProcessor where U : class
    {
        s_processors.Register<T, U>();
    }

    public static void CreateDirectory(string directory)
    {
        Directory.CreateDirectory(Path.Combine(s_assetsPath, directory));
    }

    public static void Unload(string path)
    {
        UnCache(PathUtils.FormatPath(path));
    }

    public static T Load<T>(string path)
    {
        return LoadInternal<T>(path, true).GetAwaiter().GetResult();
    }

    public static async Task<T> LoadAsync<T>(string path)
    {
        return await LoadInternal<T>(path, false);
    }

    private static async Task<T> LoadInternal<T>(string path, bool sync)
    {
        path = PathUtils.FormatPath(path);

        if (s_cache.TryGet(path, out T cachedAsset))
        {
            return cachedAsset;
        }

        if (s_processors.TryGetProcessor(typeof(T), out IProcessor<T> processor))
        {
            var result = sync ? processor.Load(path) : await processor.LoadAsync(path);

            Cache(path, result);

            return result;
        }

        return default;
    }

    public static void Save<T>(string path, T resource)
    {
        if (s_processors.TryGetProcessor(typeof(T), out IProcessor<T> processor))
        {
            processor.Save(path, resource);
        }
    }

    public static async Task SaveAsync<T>(string path, T resource)
    {
        if (s_processors.TryGetProcessor(typeof(T), out IProcessor<T> processor))
        {
            await processor.SaveAsync(path, resource);
        }
    }

    private static void Cache<T>(string key, T asset)
    {
        s_cache.TryAdd(key, asset);
    }

    private static void UnCache(string key)
    {
        s_cache.TryRemove(key);
    }

    public void Dispose()
    {
        s_processors.Dispose();
        s_cache.Dispose();
    }

    public void Initialize()
    {
        RegisterProcessor<SpriteProcessor, Sprite>();
        RegisterProcessor<Texture2DProcessor, Texture2D>();
    }

    public void PreUpdate()
    {

    }

    public void PostUpdate()
    {

    }
}

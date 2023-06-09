using System.IO;
using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed class Resources : System
{
    private Processors _processors;
    private Serializer _serializer;
    private Cache _cache;

    public Resources()
    {
        _serializer = new Serializer(Path.Join(Directory.GetCurrentDirectory(), "Assets"));
        _cache = new Cache();
    }

    public static Asset Load<T>(string path)
    {
        throw new global::System.NotImplementedException();
    }

    public static Task<Asset> LoadAsync<T>(string path)
    {
        throw new global::System.NotImplementedException();
    }

    public static bool Save<T>(string path) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public static Task<bool> SaveAsync<T>(string path) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public override void Dispose()
    {
        _serializer.Dispose();
        _cache.Clear();
    }

    public override void Initialize()
    {

    }

    public override void PreUpdate()
    {

    }

    public override void PostUpdate()
    {

    }
}

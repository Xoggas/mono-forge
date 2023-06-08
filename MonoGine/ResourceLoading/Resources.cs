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

    public override void Initialize()
    {
        
    }

    public Asset Load(string path)
    {
        throw new global::System.NotImplementedException();
    }

    public Task<Asset> LoadAsync(string path)
    {
        throw new global::System.NotImplementedException();
    }

    public bool Save<T>(string path) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public Task<bool> SaveAsync<T>(string path) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public override void PreUpdate()
    {
        
    }

    public override void PostUpdate()
    {

    }

    public override void Dispose()
    {
        _serializer.Dispose();
        _cache.Clear();
    }
}

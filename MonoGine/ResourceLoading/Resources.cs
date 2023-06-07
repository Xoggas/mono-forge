using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed class Resources : System
{
    private Serializer _serializer;
    private Dictionary<string, Asset> _assets;

    public Resources()
    {
        _serializer = new Serializer(Path.Join(Directory.GetCurrentDirectory(), "Assets"));
        _assets = new Dictionary<string, Asset>();
    }

    public override void Initialize()
    {
        
    }

    public Asset Load(string id)
    {
        throw new global::System.NotImplementedException();
    }

    public Task<Asset> LoadAsync(string id)
    {
        throw new global::System.NotImplementedException();
    }

    public bool Save<T>(string id) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public Task<bool> SaveAsync<T>(string id) where T : Asset
    {
        throw new global::System.NotImplementedException();
    }

    public override void PostUpdate()
    {

    }

    public override void Dispose()
    {

    }
}

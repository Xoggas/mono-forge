using System.Threading.Tasks;

namespace MonoGine.ContentPipeline;

public abstract class Processor : Object
{
    public Metadata GenerateDefaultMetadata(string assetPath)
    {
        throw new global::System.NotImplementedException();
    }

    public Resource Load(Metadata metadata)
    {
        throw new global::System.NotImplementedException();
    }

    public Task<Resource> LoadAsync(Metadata metadata)
    {
        throw new global::System.NotImplementedException();
    }

    public bool Save(Resource asset)
    {
        throw new global::System.NotImplementedException();
    }

    public Task<bool> SaveAsync(Resource asset)
    {
        throw new global::System.NotImplementedException();
    }
}

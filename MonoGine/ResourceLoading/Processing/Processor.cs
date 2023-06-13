using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public abstract class Processor : Object
{
    public abstract Resource CreateAsset(string assetPath);

    public abstract Resource Load(string assetPath);

    public abstract bool Save(Resource asset);

    public abstract Task<Resource> LoadAsync(string assetPath);

    public abstract Task<bool> SaveAsync(Resource asset);
}

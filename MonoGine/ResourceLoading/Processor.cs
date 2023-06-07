using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public abstract class Processor : Object
{
    public abstract void GenerateDefaultMetadata(string assetPath);
    public abstract Asset Load(string assetPath);
    public abstract bool Save(Asset asset);
    public abstract Task<Asset> LoadAsync(string assetPath);
    public abstract Task<bool> SaveAsync(Asset asset);
}

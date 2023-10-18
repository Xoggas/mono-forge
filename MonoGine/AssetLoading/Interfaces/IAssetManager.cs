namespace MonoGine.AssetLoading;

public interface IAssetManager : IObject, IInitializable
{
    public void RegisterProcessor<T>(IAssetProcessor assetProcessor) where T : class, IAsset;
    public T LoadFromFile<T>(string path) where T : class, IAsset;
    public void SaveToFile<T>(string path, T asset) where T : class, IAsset;
    public void Unload(string path);
}
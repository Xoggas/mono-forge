namespace MonoGine.AssetLoading;

public interface IAssetReader<out T> : IAssetProcessor where T : class, IAsset
{
    public T Read(IEngine engine, string path);
}
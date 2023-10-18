namespace MonoGine.AssetLoading;

public interface IAssetWriter<in T> : IAssetProcessor where T : class, IAsset
{
    public void Write(IEngine engine, string path, T resource);
}
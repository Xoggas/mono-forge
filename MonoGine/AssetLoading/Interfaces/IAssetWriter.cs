namespace MonoGine.AssetLoading;

/// <summary>
/// A base interface for asset writers.
/// </summary>
/// <typeparam name="T">The type of the input asset.</typeparam>
public interface IAssetWriter<in T> : IAssetProcessor where T : class, IAsset
{
    public void Write(IEngine engine, string localPath, T resource);
}
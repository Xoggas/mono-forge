namespace MonoGine.AssetLoading;

/// <summary>
/// A base interface for asset readers.
/// </summary>
/// <typeparam name="T">The type of the output asset.</typeparam>
public interface IAssetReader<out T> : IAssetProcessor where T : class, IAsset
{
    public T Read(IEngine engine, string localPath);
}
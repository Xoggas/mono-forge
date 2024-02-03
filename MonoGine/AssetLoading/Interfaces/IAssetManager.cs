namespace MonoGine.AssetLoading;

/// <summary>
/// A base interface for an asset manager that handles loading, unloading and saving of assets.
/// </summary>
public interface IAssetManager : IObject, IInitializable
{
    /// <summary>
    /// Registers a new processor for a specific asset.
    /// </summary>
    /// <param name="assetProcessor">The type of the processor that will process the asset data.</param>
    /// <typeparam name="T">The type of the asset the processor will be registered for.</typeparam>
    public void RegisterProcessor<T>(IAssetProcessor assetProcessor) where T : class, IAsset;

    /// <summary>
    /// Loads an asset. 
    /// </summary>
    /// <param name="path">Path to the asset.</param>
    /// <typeparam name="T">The type of the result asset that should be loaded.</typeparam>
    /// <returns></returns>
    public T LoadFromFile<T>(string path) where T : class, IAsset;

    /// <summary>
    /// Saves an asset.
    /// </summary>
    /// <param name="path">The destination asset path.</param>
    /// <param name="asset">The asset that should be saved.</param>
    /// <typeparam name="T">Type of the asset that should be saved.</typeparam>
    public void SaveToFile<T>(string path, T asset) where T : class, IAsset;

    /// <summary>
    /// Unloads the asset. 
    /// </summary>
    /// <param name="path">The path of the asset to unload.</param>
    public void Unload(string path);
}
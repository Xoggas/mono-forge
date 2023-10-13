namespace MonoGine.ResourceLoading;

/// <summary>
/// Represents an interface for a resource manager.
/// </summary>
public interface IResourceManager : IObject, IInitializable
{
    /// <summary>
    /// Registers a processor for handling a specific resource type.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    /// <param name="processor">The processor responsible for handling the resource type.</param>
    public void RegisterProcessor<T>(IProcessor<T> processor) where T : class;

    /// <summary>
    /// Loads a resource of the specified type from the given path.
    /// </summary>
    /// <typeparam name="T">The type of resource to load.</typeparam>
    /// <param name="path">The path to the resource.</param>
    /// <returns>The loaded resource.</returns>
    public T Load<T>(string path) where T : class;

    /// <summary>
    /// Saves a resource of the specified type to the given path.
    /// </summary>
    /// <typeparam name="T">The type of resource to save.</typeparam>
    /// <param name="path">The path to save the resource to.</param>
    /// <param name="resource">The resource to save.</param>
    public void Save<T>(string path, T resource) where T : class;

    /// <summary>
    /// Unloads a resource at the given path.
    /// </summary>
    /// <param name="path">The path to the resource to unload.</param>
    public void Unload(string path);
}

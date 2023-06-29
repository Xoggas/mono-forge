namespace MonoGine.Resources;

/// <summary>
/// Represents an interface for a resource processor.
/// </summary>
public interface IProcessor
{
    /// <summary>
    /// Loads a resource of the specified type from the given path using the provided engine.
    /// </summary>
    /// <typeparam name="T">The type of resource to load.</typeparam>
    /// <param name="engine">The engine used for loading the resource.</param>
    /// <param name="path">The path to the resource.</param>
    /// <returns>The loaded resource, or null if it failed to load.</returns>
    public T Load<T>(IEngine engine, string path) where T : class;

    /// <summary>
    /// Saves a resource of the specified type to the given path using the provided engine.
    /// </summary>
    /// <typeparam name="T">The type of resource to save.</typeparam>
    /// <param name="engine">The engine used for saving the resource.</param>
    /// <param name="path">The path to save the resource to.</param>
    /// <param name="resource">The resource to save.</param>
    public void Save<T>(IEngine engine, string path, T resource) where T : class;
}

namespace MonoGine.ResourceLoading;

public interface IResourceReader<out T> : IResourceProcessor where T : class, IResource
{
    /// <summary>
    /// Loads a resource of the specified type from the given path using the provided engine.
    /// </summary>
    /// <typeparam name="T">The type of resource to load.</typeparam>
    /// <param name="engine">The engine used for loading the resource.</param>
    /// <param name="path">The path to the resource.</param>
    /// <returns>The loaded resource, or null if it failed to load.</returns>
    public T Read(IEngine engine, string path);
}
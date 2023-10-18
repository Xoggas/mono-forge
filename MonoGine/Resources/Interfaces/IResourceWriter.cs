namespace MonoGine.ResourceLoading;

public interface IResourceWriter<in T> : IResourceProcessor where T : class, IResource
{
    /// <summary>
    /// Saves a resource of the specified type to the given path using the provided engine.
    /// </summary>
    /// <typeparam name="T">The type of resource to save.</typeparam>
    /// <param name="engine">The engine used for saving the resource.</param>
    /// <param name="path">The path to save the resource to.</param>
    /// <param name="resource">The resource to save.</param>
    public void Write(IEngine engine, string path, T resource);
}
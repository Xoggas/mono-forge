namespace MonoGine;

/// <summary>
/// Represents an interface for objects that can be updated.
/// </summary>
public interface IUpdatable
{
    /// <summary>
    /// Updates the object using the specified engine.
    /// </summary>
    /// <param name="engine">The engine used for the update.</param>
    public void Update(IEngine engine);
}

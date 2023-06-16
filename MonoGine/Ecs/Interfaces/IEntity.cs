using System.Collections.Generic;

namespace MonoGine.Ecs;

public interface IEntity : IEntityComponent
{
    /// <summary>
    /// Adds a component of type T to the entity.
    /// </summary>
    /// <typeparam name="T">The type of component to add.</typeparam>
    /// <returns>The added component.</returns>
    public T AddComponent<T>() where T : IComponent;

    /// <summary>
    /// Gets the first component of type T from the entity.
    /// </summary>
    /// <typeparam name="T">The type of component to retrieve.</typeparam>
    /// <returns>The first component of the specified type, or null if not found.</returns>
    public T? GetFirstComponent<T>() where T : IComponent;

    /// <summary>
    /// Gets all components of type T from the entity.
    /// </summary>
    /// <typeparam name="T">The type of components to retrieve.</typeparam>
    /// <returns>An enumerable collection of components of the specified type.</returns>
    public IEnumerable<T> GetComponentsOfType<T>() where T : IComponent;

    /// <summary>
    /// Destroys all components of type T in the entity.
    /// </summary>
    /// <typeparam name="T">The type of components to destroy.</typeparam>
    public void DestroyComponentsOfType<T>() where T : IComponent;
}
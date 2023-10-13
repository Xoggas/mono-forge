using System.Collections.Generic;

namespace MonoGine.Ecs;

public interface IWorld : IObject, IUpdatable
{
    /// <summary>
    /// Creates a new entity of the specified type and adds it to the world.
    /// </summary>
    /// <typeparam name="T">The type of entity to create.</typeparam>
    /// <returns>The created entity.</returns>
    public T CreateEntity<T>() where T : IEntity, new();

    /// <summary>
    /// Gets all entities of the specified type from the world.
    /// </summary>
    /// <typeparam name="T">The type of entities to retrieve.</typeparam>
    /// <returns>An IEnumerable of entities of the specified type.</returns>
    public IEnumerable<T> GetEntitiesOfType<T>() where T : IEntity;

    /// <summary>
    /// Destroys all entities of the specified type in the world.
    /// </summary>
    /// <typeparam name="T">The type of entities to destroy.</typeparam>
    public void DestroyEntitiesOfType<T>() where T : IEntity;

    /// <summary>
    /// Destroys all entities in the world.
    /// </summary>
    public void DestroyAllEntities();
}

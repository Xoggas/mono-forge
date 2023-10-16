using System.Collections.Generic;

namespace MonoGine.Ecs;

public interface IWorld : IObject, IUpdatable
{
    /// <summary>
    /// Adds the entity to the update list.
    /// </summary>
    /// <param name="entity"></param>
    public void AddEntity(IEntity entity);

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
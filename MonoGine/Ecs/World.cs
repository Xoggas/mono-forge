using MonoGine.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGine.Ecs;

/// <summary>
/// Represents a game world that contains entities.
/// </summary>
public class World : IWorld
{
    private List<IEntity> _entities;

    /// <summary>
    /// Initializes a new instance of the World class.
    /// </summary>
    internal World()
    {
        _entities = new List<IEntity>();
    }

    /// <summary>
    /// Creates a new entity of the specified type and adds it to the world.
    /// </summary>
    /// <typeparam name="T">The type of entity to create.</typeparam>
    /// <returns>The created entity.</returns>
    public T CreateEntity<T>() where T : IEntity
    {
        var entity = Activator.CreateInstance<T>();

        _entities.Add(entity);

        return entity;
    }

    /// <summary>
    /// Gets all entities of the specified type from the world.
    /// </summary>
    /// <typeparam name="T">The type of entities to retrieve.</typeparam>
    /// <returns>An IEnumerable of entities of the specified type.</returns>
    public IEnumerable<T> GetEntitiesOfType<T>() where T : IEntity
    {
        return (IEnumerable<T>)_entities.FindAll(entity => entity is T);
    }

    /// <summary>
    /// Destroys all entities of the specified type in the world.
    /// </summary>
    /// <typeparam name="T">The type of entities to destroy.</typeparam>
    public void DestroyEntitiesOfType<T>() where T : IEntity
    {
        foreach (var entity in GetEntitiesOfType<T>())
        {
            entity.Destroy();
        }
    }

    /// <summary>
    /// Destroys all entities in the world.
    /// </summary>
    public void DestroyAllEntities()
    {
        foreach (var entity in _entities)
        {
            entity.Destroy();
        }
    }

    /// <summary>
    /// Updates all entities in the world.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    public void Update(IEngine engine)
    {
        foreach (var entity in _entities)
        {
            if (ShouldSkip(entity))
            {
                continue;
            }

            entity.Update(engine);
        }

        RemoveDestroyedEntities();
    }

    /// <summary>
    /// Draws all entities in the world.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    /// <param name="batcher">The batcher used for rendering.</param>
    public void Draw(IEngine engine, IBatcher batcher)
    {
        foreach (var entity in _entities)
        {
            if (ShouldSkip(entity))
            {
                continue;
            }

            entity.Draw(engine, batcher);
        }
    }

    /// <summary>
    /// Disposes the world and all its entities.
    /// </summary>
    public void Dispose()
    {
        foreach (var entity in _entities)
        {
            entity.Dispose();
        }
    }

    private bool ShouldSkip(IEntity entity)
    {
        return entity.IsDestroyed || !entity.IsActive;
    }

    private void RemoveDestroyedEntities()
    {
        _entities.RemoveAll(entity =>
        {
            if (entity.IsDestroyed)
            {
                entity.Dispose();
                return true;
            }

            return false;
        });
    }
}

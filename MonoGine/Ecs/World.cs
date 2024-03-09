using System.Collections.Generic;
using System.Linq;

namespace MonoGine.Ecs;

public class World : IObject, IUpdatable
{
    private readonly List<IEntity> _entities;

    internal World()
    {
        _entities = new List<IEntity>();
    }

    public void AddEntity(IEntity entity)
    {
        _entities.Add(entity);
    }

    public IEnumerable<T> GetEntitiesOfType<T>() where T : IEntity
    {
        return (IEnumerable<T>)from entity in _entities where entity is T select entity;
    }

    public void DestroyEntitiesOfType<T>() where T : IEntity
    {
        foreach (T entity in GetEntitiesOfType<T>())
        {
            entity.Destroy();
        }
    }

    public void DestroyAllEntities()
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];
            entity.Destroy();
        }
    }

    public void Update(IGame game, float deltaTime)
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];

            if (ShouldSkip(entity))
            {
                continue;
            }

            entity.Update(game, deltaTime);
        }

        RemoveDestroyedEntities();
    }

    public void Dispose()
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];
            entity.Dispose();
        }
    }

    private static bool ShouldSkip(IEntityComponent entity)
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
            }

            return entity.IsDestroyed;
        });
    }
}
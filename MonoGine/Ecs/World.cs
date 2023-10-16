using System.Collections.Generic;

namespace MonoGine.Ecs;

public class World : IWorld
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
        return (IEnumerable<T>)_entities.FindAll(entity => entity is T);
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

    public void Update(IEngine engine)
    {
        for (var i = 0; i < _entities.Count; i++)
        {
            IEntity entity = _entities[i];

            if (ShouldSkip(entity))
            {
                continue;
            }

            entity.Update(engine);
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
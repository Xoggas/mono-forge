using System;
using System.Collections.Generic;

namespace MonoGine.Ecs;

public class World : Object
{
    private List<Entity> _activeEntities = new List<Entity>();

    public T CreateEntity<T>() where T : Entity
    {
        T entity = (T)Activator.CreateInstance(typeof(T), true);

        _activeEntities.Add(entity);

        return entity;
    }

    public List<T> GetAllEntitiesOfType<T>() where T : Entity
    {
        List<T> entities = _activeEntities.FindAll(x => x.GetType().Equals(typeof(T))).ConvertAll(x => x as T);

        return entities;
    }

    public void DestroyAllEntities()
    {
        foreach (Entity entity in _activeEntities)
        {
            entity.Destroy();
        }
    }

    public void DestroyAllEntitiesOfType<T>() where T : Entity
    {
        List<T> entities = GetAllEntitiesOfType<T>();

        foreach (Entity entity in entities)
        {
            entity.Destroy();
        }
    }

    public void Update()
    {
        UpdateEntities();
        RemoveDestroyedEntities();
    }

    public override void Dispose()
    {
        DestroyAllEntities();
        RemoveDestroyedEntities();
    }

    private void UpdateEntities()
    {
        for (int i = 0; i < _activeEntities.Count; i++)
        {
            var entity = _activeEntities[i];

            if (!entity.Enabled || entity.Destroyed)
            {
                continue;
            }

            entity.Update();
        }
    }

    private void RemoveDestroyedEntities()
    {
        for (int i = 0; i < _activeEntities.Count; i++)
        {
            var entity = _activeEntities[i];

            if (entity.Destroyed)
            {
                entity.Dispose();

                _activeEntities.RemoveAt(i);
            }
        }
    }
}

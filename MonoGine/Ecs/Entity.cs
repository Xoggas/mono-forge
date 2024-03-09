using System;
using System.Collections.Generic;

namespace MonoGine.Ecs;

public abstract class Entity : IEntity
{
    public bool Started { get; private set; }
    public bool IsDestroyed { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<IComponent> _components = new();

    public void AddComponent(IComponent component)
    {
        _components.Add(component);
    }

    public bool ContainsComponent(IComponent component)
    {
        return _components.Contains(component);
    }

    public T? GetFirstComponent<T>() where T : IComponent
    {
        return (T?)_components.Find(component => component is T);
    }

    public IEnumerable<T> GetComponentsOfType<T>() where T : IComponent
    {
        return (IEnumerable<T>)_components.FindAll(component => component is T);
    }

    public void DestroyComponentsOfType<T>() where T : IComponent
    {
        foreach (T component in GetComponentsOfType<T>())
        {
            component.Destroy();
        }
    }

    public virtual void Start(IGame game)
    {
        Started = true;
    }

    public virtual void Update(IGame game, float deltaTime)
    {
        if (!Started)
        {
            Start(game);
            return;
        }

        for (var i = 0; i < _components.Count; i++)
        {
            IComponent component = _components[i];

            if (component.IsDead)
            {
                continue;
            }

            component.Update(game, deltaTime);
        }

        RemoveDestroyedComponents();
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;

        foreach (IComponent component in _components)
        {
            component.Destroy();
        }
    }

    public virtual void Dispose()
    {
        foreach (IComponent component in _components)
        {
            component.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    private void RemoveDestroyedComponents()
    {
        _components.RemoveAll(component =>
        {
            if (!component.IsDestroyed)
            {
                return false;
            }

            component.Dispose();
            return true;
        });
    }
}
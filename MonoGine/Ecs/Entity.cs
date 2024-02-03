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

    public virtual void Start(IEngine engine)
    {
        Started = true;
    }

    public virtual void Update(IEngine engine)
    {
        if (!Started)
        {
            Start(engine);
            return;
        }

        for (var i = 0; i < _components.Count; i++)
        {
            IComponent component = _components[i];

            if (ShouldSkip(component))
            {
                continue;
            }

            component.Update(engine);
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
    }

    private bool ShouldSkip(IComponent component)
    {
        return component.IsDestroyed || !component.IsActive;
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
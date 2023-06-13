using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonoGine.Ecs;

public class Entity : EntityComponentBase
{
    private List<Component> _components = new List<Component>();

    protected Entity()
    {

    }

    public override void Update()
    {
        base.Update();

        UpdateComponents();
        RemoveDestroyedComponents();
    }

    public T AddComponent<T>() where T : Component
    {
        T component = (T)Activator.CreateInstance(typeof(T), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { this }, null);

        _components.Add(component);

        return component;
    }

    public void AddComponent(Component component)
    {
        _components.Add(component);
    }

    public void TryDestroyAllComponentsOfType<T>() where T : Component
    {
        if (TryGetComponents(out List<T> components))
        {
            foreach (var component in components)
            {
                component.Destroy();
            }
        }
    }

    public T GetComponent<T>() where T : Component
    {
        TryGetComponent(out T component);

        return component;
    }

    public bool TryGetComponent<T>(out T component) where T : Component
    {
        component = (T)_components.Find(x => x is T);

        return component != null;
    }

    public bool TryGetComponents<T>(out List<T> components) where T : Component
    {
        List<Component> rawComponents = _components.FindAll(x => x is T);

        components = rawComponents.Count > 0 ? rawComponents.ConvertAll(x => x as T) : null;

        return components != null;
    }

    public override void Dispose()
    {
        foreach (var component in _components)
        {
            component.Dispose();
        }

        _components.Clear();
    }

    private void UpdateComponents()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var component = _components[i];

            if (!component.Enabled || component.Destroyed)
            {
                continue;
            }

            component.Update();
        }
    }

    private void RemoveDestroyedComponents()
    {
        for (int i = 0; i < _components.Count; i++)
        {
            var component = _components[i];

            if (component.Destroyed)
            {
                component.Dispose();

                _components.RemoveAt(i);
            }
        }
    }
}


using MonoGine.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGine.Ecs;

/// <summary>
/// Represents an abstract entity.
/// </summary>
public abstract class Entity : IEntity
{
    private List<IComponent> _components;

    /// <summary>
    /// Initializes a new instance of the Entity class.
    /// </summary>
    protected Entity()
    {
        _components = new List<IComponent>();
    }

    /// <summary>
    /// Gets a value indicating whether the entity has been started.
    /// </summary>
    public bool Started { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the entity has been destroyed.
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the entity is active.
    /// </summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>
    /// Adds a component of type T to the entity.
    /// </summary>
    /// <typeparam name="T">The type of component to add.</typeparam>
    /// <returns>The added component.</returns>
    public T AddComponent<T>() where T : notnull, IComponent
    {
        var component = (T?)Activator.CreateInstance(typeof(T), this);

        if (component != null)
        {
            _components.Add(component);

            return component;
        }
        else
        {
            throw new Exception("Error occured when creating a new component!");
        }
    }

    /// <summary>
    /// Gets the first component of type T from the entity.
    /// </summary>
    /// <typeparam name="T">The type of component to retrieve.</typeparam>
    /// <returns>The first component of the specified type, or null if not found.</returns>
    public T? GetFirstComponent<T>() where T : IComponent
    {
        return (T?)_components.Find(component => component is T);
    }

    /// <summary>
    /// Gets all components of type T from the entity.
    /// </summary>
    /// <typeparam name="T">The type of components to retrieve.</typeparam>
    /// <returns>An enumerable collection of components of the specified type.</returns>
    public IEnumerable<T> GetComponentsOfType<T>() where T : IComponent
    {
        return (IEnumerable<T>)_components.FindAll(component => component is T);
    }

    /// <summary>
    /// Destroys all components of type T in the entity.
    /// </summary>
    /// <typeparam name="T">The type of components to destroy.</typeparam>
    public void DestroyComponentsOfType<T>() where T : IComponent
    {
        foreach (var component in GetComponentsOfType<T>())
        {
            component.Destroy();
        }
    }

    /// <summary>
    /// Starts the entity and components (even disabled components are started).
    /// </summary>
    /// <param name="engine">The engine used for starting the entity.</param>
    public virtual void Start(IEngine engine)
    {
        Started = true;
    }

    /// <summary>
    /// Updates the entity.
    /// </summary>
    /// <param name="engine">The engine used for updating the entity.</param>
    public virtual void Update(IEngine engine)
    {
        if (!Started)
        {
            Start(engine);
        }

        foreach (var component in _components)
        {
            if (ShouldSkip(component))
            {
                continue;
            }

            component.Update(engine);
        }

        RemoveDestroyedComponents();
    }

    /// <summary>
    /// Draws the entity.
    /// </summary>
    /// <param name="engine">The engine used for drawing the entity.</param>
    /// <param name="batcher">The batcher used for batching draw calls.</param>
    public virtual void Draw(IEngine engine, IBatcher batcher)
    {
        foreach (var component in _components)
        {
            if (ShouldSkip(component))
            {
                continue;
            }

            component.Draw(engine, batcher);
        }
    }

    /// <summary>
    /// Destroys the entity and its components.
    /// </summary>
    public virtual void Destroy()
    {
        IsDestroyed = true;

        foreach (var component in _components)
        {
            component.Destroy();
        }
    }

    /// <summary>
    /// Disposes of the entity and its components.
    /// </summary>
    public virtual void Dispose()
    {
        foreach (var component in _components)
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
            if (component.IsDestroyed)
            {
                component.Dispose();
                return true;
            }

            return false;
        });
    }
}

using MonoGine.Rendering;
using MonoGine.Rendering.Batching;

namespace MonoGine.Ecs;

/// <summary>
/// Represents a base class for components.
/// </summary>
public abstract class Component : IComponent
{
    /// <summary>
    /// Gets the entity to which this component belongs.
    /// </summary>
    public IEntity? Entity { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the component has been started.
    /// </summary>
    public bool Started { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the component has been destroyed.
    /// </summary>
    public bool IsDestroyed { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the component is currently active.
    /// </summary>
    public bool IsActive { get; private set; } = true;

    /// <summary>
    /// Initializes a new instance of the Component class with the specified entity.
    /// </summary>
    /// <param name="entity">The entity to which this component belongs.</param>
    protected Component(IEntity entity)
    {
        Entity = entity;
    }

    /// <summary>
    /// Called when the component is started, providing the engine context.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    public virtual void Start(IEngine engine)
    {
        Started = true;
    }

    /// <summary>
    /// Updates the component's logic once per frame.
    /// </summary>
    /// <param name="engine">The engine used for the game.</param>
    public virtual void Update(IEngine engine)
    {
        if (!Started)
        {
            Start(engine);
        }
    }

    /// <summary>
    /// Marks the component as destroyed and removes its reference to the entity.
    /// </summary>
    public virtual void Destroy()
    {
        IsDestroyed = true;
        Entity = null;
    }

    /// <summary>
    /// Performs any necessary cleanup operations for the component.
    /// </summary>
    public virtual void Dispose()
    {
    }
}

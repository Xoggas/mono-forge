namespace MonoGine.Ecs;

public interface IEntityComponent : IObject, IUpdatable, IDestroyable
{
    /// <summary>
    /// Gets a value indicating whether the entity has been started.
    /// </summary>
    public bool Started { get; }

    /// <summary>
    /// Gets a value indicating whether the entity has been destroyed.
    /// </summary>
    public bool IsDestroyed { get; }

    /// <summary>
    /// Gets a value indicating whether the entity is active.
    /// </summary>
    public bool IsActive { get; }

    internal bool IsDead => IsDestroyed || !IsActive;

    /// <summary>
    /// Starts the entity and components (even disabled components are started).
    /// </summary>
    /// <param name="engine">The engine used for starting the entity.</param>
    public void Start(IEngine engine);
}
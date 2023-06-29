namespace MonoGine.Ecs;

public interface IEntityComponent : IObject, IUpdatable
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

    /// <summary>
    /// Starts the entity and components (even disabled components are started).
    /// </summary>
    /// <param name="engine">The engine used for starting the entity.</param>
    public void Start(IEngine engine);

    /// <summary>
    /// Destroys the entity and its components.
    /// </summary>
    public void Destroy();
}
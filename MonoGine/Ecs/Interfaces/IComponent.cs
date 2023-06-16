namespace MonoGine.Ecs;

public interface IComponent : IEntityComponent
{
    /// <summary>
    /// Gets the entity to which this component belongs.
    /// </summary>
    public IEntity? Entity { get; }
}
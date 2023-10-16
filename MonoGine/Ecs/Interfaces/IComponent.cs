namespace MonoGine.Ecs;

public interface IComponent : IEntityComponent
{
    public bool IsAttachedToEntity(IEntity entity);
}
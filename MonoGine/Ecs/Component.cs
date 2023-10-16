namespace MonoGine.Ecs;

public abstract class Component : IComponent
{
    public bool Started { get; private set; }
    public bool IsDestroyed { get; private set; }
    public bool IsActive { get; private set; } = true;

    public virtual void Start(IEngine engine)
    {
        Started = true;
    }

    public bool IsAttachedToEntity(IEntity entity)
    {
        return entity.ContainsComponent(this);
    }

    public virtual void Update(IEngine engine)
    {
        if (!Started)
        {
            Start(engine);
        }
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }

    public virtual void Dispose()
    {
    }
}
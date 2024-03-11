using System;

namespace MonoForge.Ecs;

public abstract class Component : IComponent
{
    public bool Started { get; private set; }
    public bool IsDestroyed { get; private set; }
    public bool IsActive { get; private set; } = true;

    public virtual void Start(GameBase gameBase)
    {
        Started = true;
    }

    public bool IsAttachedToEntity(IEntity entity)
    {
        return entity.ContainsComponent(this);
    }

    public virtual void Update(GameBase gameBase, float deltaTime)
    {
        if (!Started)
        {
            Start(gameBase);
        }
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
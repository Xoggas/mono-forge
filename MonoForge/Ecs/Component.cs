using System;

namespace MonoForge.Ecs;

public abstract class Component : IComponent
{
    public bool Started { get; private set; }
    public bool IsDestroyed { get; private set; }
    public bool IsActive { get; private set; } = true;

    public virtual void Start(IGame game)
    {
        Started = true;
    }

    public bool IsAttachedToEntity(IEntity entity)
    {
        return entity.ContainsComponent(this);
    }

    public virtual void Update(IGame game, float deltaTime)
    {
        if (!Started)
        {
            Start(game);
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
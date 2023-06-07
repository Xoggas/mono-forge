﻿namespace MonoGine.Ecs;

public class Component : EntityComponentBase
{
    private Component(Entity entity)
    {
        Entity = entity;
    }

    public Entity Entity { get; private set; }

    public override void Dispose()
    {
        Entity = null;
    }
}

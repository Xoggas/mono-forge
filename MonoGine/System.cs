namespace MonoGine;

public abstract class System : Object
{
    internal abstract void Initialize();
    internal abstract void PreUpdate();
    internal abstract void PostUpdate();
}

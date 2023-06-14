namespace MonoGine;

public interface ISystem : IObject
{
    public void Initialize();
    public void PreUpdate();
    public void PostUpdate();
}

namespace MonoGine;

public interface ISystem : IObject, IUpdatable
{
    public void Initialize(Engine engine);
}

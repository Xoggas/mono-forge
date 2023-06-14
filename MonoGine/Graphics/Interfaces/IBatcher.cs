namespace MonoGine.Graphics;

public interface IBatcher : IObject
{
    public void Clear(Engine engine);
    public void Draw(Engine engine);
}

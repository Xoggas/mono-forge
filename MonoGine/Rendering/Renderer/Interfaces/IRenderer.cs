using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public interface IRenderer : IObject
{
    public void Draw(IEngine engine, IScene scene);
}

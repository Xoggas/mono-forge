using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public interface IRenderer : IObject
{
    public Shader? PostProcessingEffect { get; set; }
    public void Draw(IEngine engine, IScene scene);
}

using MonoForge.Rendering.Batching;
using MonoForge.SceneManagement;

namespace MonoForge.Rendering;

public interface IRenderer
{
    public RenderConfig Config { get; set; }
    public void SetBatcher(IBatcher batcher);
    public void Draw(IGame game, Scene scene);
}
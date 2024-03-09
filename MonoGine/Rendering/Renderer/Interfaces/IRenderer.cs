using MonoGine.Rendering.Batching;
using MonoGine.SceneManagement;

namespace MonoGine.Rendering;

public interface IRenderer
{
    public RenderConfig Config { get; set; }
    public void SetBatcher(IBatcher batcher);
    public void Draw(IGame game, Scene scene);
}
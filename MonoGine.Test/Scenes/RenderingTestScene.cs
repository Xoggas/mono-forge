using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class RenderingTestScene : Scene
{
    protected override void OnLoadResources(IEngine engine)
    {
        engine.AssetManager.LoadFromFile<Sprite>("Rectangle.png");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        var spriteNode = new SpriteNode
        {
            Transform =
            {
            }
        };
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}
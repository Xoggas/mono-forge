using Microsoft.Xna.Framework;
using MonoForge.Rendering;
using MonoForge.SceneGraph;
using MonoForge.SceneManagement;

namespace MonoForge.Test;

public sealed class RenderingTestScene : Scene
{
    private Sprite _sprite = default!;

    protected override void OnLoadResources(GameBase gameBase)
    {
    }

    protected override void OnLoad(GameBase gameBase, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;

        var spriteNode = new SpriteNode
        {
            Transform =
            {
                Position = new Vector2(640, 360),
                Scale = new Vector2(100, 100)
            },
            Sprite = _sprite,
            Color = Color.DeepPink
        };

        Root.AddChild(spriteNode);
    }

    protected override void OnUnload(GameBase gameBase, object[]? args)
    {
    }
}
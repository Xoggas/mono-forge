using Microsoft.Xna.Framework;
using MonoForge.Rendering;
using MonoForge.SceneGraph;
using MonoForge.SceneManagement;
using MonoForge.SceneManagement.Interfaces;

namespace MonoForge.Test;

public sealed class RenderingTestScene : Scene
{
    public RenderingTestScene(GameBase gameBase, ISceneLoadingArgs args) : base(gameBase, args)
    {
        Camera.BackgroundColor = Color.Gray;

        var spriteNode = new SpriteNode
        {
            Transform =
            {
                Position = new Vector2(640, 360),
                Scale = new Vector2(100, 100)
            },
            Sprite = gameBase.ContentManager.Load<Sprite>("Content/Rectangle"),
            Color = Color.DeepPink
        };

        Root.AddChild(spriteNode);
    }

    protected override void OnUnload(GameBase gameBase)
    {
        gameBase.ContentManager.UnloadAsset("Content/Rectangle");
    }
}
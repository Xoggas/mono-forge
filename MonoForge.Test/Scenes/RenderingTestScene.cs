using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoForge.Rendering;
using MonoForge.SceneGraph;
using MonoForge.SceneManagement;
using MonoForge.Extensions;

namespace MonoForge.Test;

public sealed class RenderingTestScene : Scene
{
    protected override void OnLoadResources(IGame game)
    {
    }

    protected override void OnLoad(IGame game, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;

        var normalSpriteNode = new SpriteNode
        {
            Transform =
            {
                Position = new Vector2(640, 360),
                Scale = new Vector2(100, 100),
                Depth = 1f
            },
            Sprite = game.ContentManager.Load<Sprite>("Sprites/Rectangle.png")
        };

        var shader = game.ContentManager.Load<Effect>("Shaders/Brightness.shader").ToShader();

        var shadedSpriteNode = new SpriteNode
        {
            Transform =
            {
                Position = new Vector2(600, 300),
                Scale = new Vector2(100, 100),
                Depth = 2f
            },
            Color = Color.DeepPink,
            Sprite = game.ContentManager.Load<Sprite>("Sprites/Rectangle.png"),
            Shader = shader
        };

        shader.Properties.Set("Brightness", 0.5f);

        Root.AddChild(normalSpriteNode);
        Root.AddChild(shadedSpriteNode);
    }

    protected override void OnUnload(IGame game, object[]? args)
    {
    }
}
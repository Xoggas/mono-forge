using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGine.SceneGraph.Nodes;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    public override void Update(IEngine engine)
    {
        base.Update(engine);

        if (engine.Input.Keyboard.IsPressed(Keys.B))
        {
            new SpriteNode
            {
                Texture = engine.ResourceManager.Load<Texture2D>("Rect.png"),
                Transform =
                {
                    Position = new Vector2(Random.Shared.NextSingle() * 1280, Random.Shared.NextSingle() * 720),
                    Depth = engine.Time.ElapsedTime,
                    Scale = Vector2.One * 80
                }
            }.SetParent(Root);
        }
    }

    protected override void OnLoadResources(IEngine engine)
    {
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}

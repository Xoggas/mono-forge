using System;
using Microsoft.Xna.Framework;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    private Sprite _sprite = default!;

    protected override void OnLoadResources(IEngine engine)
    {
        _sprite = engine.AssetManager.LoadFromFile<Sprite>("Rect.png");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;
        CreateSprite();
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }

    private void CreateSprite()
    {
        var sprite = new SpriteNode
        {
            Sprite = _sprite,
            Transform =
            {
                Position = new Vector2(Random.Shared.NextSingle() * 1280f, Random.Shared.NextSingle() * 720f),
                Scale = Vector2.One * 100f
            }
        };

        sprite.SetParent(Root);
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    private Texture2D _texture = null!;
    private Effect _effect = null!;

    protected override void OnLoadResources(IEngine engine)
    {
        _texture = engine.ResourceManager.Load<Texture2D>("Rect.png");
        _effect = engine.ResourceManager.Load<Effect>("Shaders/Brightness.shader");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        for (var i = 0; i < 5000; i++)
        {
            var sprite = new SpriteNode
            {
                Texture = _texture,
                Transform =
                {
                    Position = new Vector2(Random.Shared.NextSingle() * 1280f, Random.Shared.NextSingle() * 720f),
                    Scale = Vector2.One * 100f,
                    Depth = engine.Time.ElapsedTime
                },
                Shader = new Shader(_effect)
            };

            sprite.SetParent(Root);

            var spriteEntity = World.CreateEntity<SpriteEntity>();
            spriteEntity.Sprite = sprite;
            spriteEntity.Offset = i * 10f;
        }
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}

public sealed class SpriteEntity : Entity
{
    public SpriteNode Sprite = null!;
    public float Offset;

    private readonly FloatProperty _brightnessProperty = new("Brightness", 0f);

    public override void Start(IEngine engine)
    {
        base.Start(engine);
        Sprite.Shader?.Properties.Add(_brightnessProperty);
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);
        _brightnessProperty.Value = MathF.Abs(MathF.Sin(engine.Time.ElapsedTime + Offset));
    }

    public override void Dispose()
    {
        base.Dispose();
        Sprite.Shader?.Dispose();
        Sprite.Dispose();
    }
}
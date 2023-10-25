using System;
using Microsoft.Xna.Framework;
using MonoGine.Animations;
using MonoGine.Ecs;
using MonoGine.Rendering;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    private Sprite _rectangle = default!;
    private Sprite _rectangleWithBlackInner = default!;
    private Shader _brightnessShader = default!;
    private AnimationClip _animationClip = default!;

    protected override void OnLoadResources(IEngine engine)
    {
        _rectangle = engine.AssetManager.LoadFromFile<Sprite>("Rectangle.png");
        _rectangleWithBlackInner = engine.AssetManager.LoadFromFile<Sprite>("RectangleWithBlackInner.png");
        _brightnessShader = engine.AssetManager.LoadFromFile<Shader>("Shaders/Brightness.shader");
        _animationClip = engine.AssetManager.LoadFromFile<AnimationClip>("Animations/square.json");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        for (var i = 0; i < 10; i++)
        {
            CreateLine(new Vector2(640, 360), 36f * i);
        }
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }

    private void CreateLine(Vector2 position, float rotation)
    {
        var delay = 75;
        var curvature = 4f;

        for (var i = 50; i > 0; i--)
        {
            CreateSprite(position, rotation, i * delay / 1000f);
            position.X += 25f * 1.1f * MathF.Cos(MathHelper.ToRadians(rotation));
            position.Y += 25f * 1.1f * MathF.Sin(MathHelper.ToRadians(rotation));
            rotation += curvature;
        }
    }

    private void CreateSprite(Vector2 position, float rotation, float time)
    {
        var rootNode = new Node
        {
            Transform =
            {
                Position = position,
                Scale = Vector2.One * 25f,
                Rotation = Vector3.Backward * rotation,
                Depth = 0
            }
        };

        var warningSprite = new SpriteNode
        {
            Name = "warning",
            Sprite = _rectangle,
            Shader = _brightnessShader.DeepCopy()
        };

        var activeSprite = new SpriteNode
        {
            Name = "active",
            Sprite = _rectangleWithBlackInner,
            Shader = _brightnessShader.DeepCopy(),
            Transform =
            {
                Depth = 1
            }
        };

        rootNode.SetParent(Root);
        warningSprite.SetParent(rootNode);
        activeSprite.SetParent(rootNode);

        World.AddEntity(new SquareEntity(rootNode, _animationClip, time));
    }
}

public class SquareEntity : Entity
{
    private readonly Node _rootNode;
    private readonly SpriteNode _warningNode;
    private readonly AnimationClip _animationClip;
    private readonly float _time;

    public SquareEntity(Node root, AnimationClip clip, float time)
    {
        _rootNode = root;
        _warningNode = (SpriteNode)root.FindChildByName("warning")!;
        _animationClip = clip;
        _time = time % 2f;
    }

    public override void Start(IEngine engine)
    {
        base.Start(engine);

        var animator = new Animator(_rootNode, _animationClip);

        animator.Time = _time;

        animator.Play();

        AddComponent(animator);
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        var limit = 0.1f;
        var speed = 15f;
        var brightness = MathF.Round(MathF.Abs(MathF.Sin(_time * speed + engine.Time.ElapsedTime * speed))) * limit;

        _warningNode.Shader?.Properties.Set("Brightness", brightness);
    }
}
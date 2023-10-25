using System;
using Microsoft.Xna.Framework;
using MonoGine.Animations;
using MonoGine.Audio;
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
    private AudioClip _audioClip = default!;

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        //Camera.Position += new Vector2(1, 0);
    }

    protected override void OnLoadResources(IEngine engine)
    {
        _rectangle = engine.AssetManager.LoadFromFile<Sprite>("Rectangle.png");
        _rectangleWithBlackInner = engine.AssetManager.LoadFromFile<Sprite>("RectangleWithBlackInner.png");
        _brightnessShader = engine.AssetManager.LoadFromFile<Shader>("Shaders/Brightness.shader");
        _animationClip = engine.AssetManager.LoadFromFile<AnimationClip>("Animations/square.json");
        _audioClip = engine.AssetManager.LoadFromFile<AudioClip>("Liftoff.mp3");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        IAudioSource source = engine.AudioManager.Master.CreateSource();

        source.Clip = _audioClip;

        source.Play();

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
            Sprite = _rectangle
        };

        var activeSprite = new SpriteNode
        {
            Name = "active",
            Sprite = _rectangleWithBlackInner,
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
}
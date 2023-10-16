using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Animations;
using MonoGine.Ecs;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    private Texture2D _texture = default!;

    protected override void OnLoadResources(IEngine engine)
    {
        _texture = engine.ResourceManager.Load<Texture2D>("Rect.png");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;

        var sprite = new SpriteNode
        {
            Texture = _texture,
            Transform =
            {
                Position = new Vector2(640f, 360f),
                Scale = Vector2.One * 100f
            }
        };

        sprite.SetParent(Root);

        World.AddEntity(new SpriteEntity(sprite));
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}

public sealed class SpriteEntity : Entity
{
    private readonly Node _node;
    private readonly Sequence _xSequence;
    private readonly Sequence _ySequence;

    public SpriteEntity(Node node)
    {
        _node = node;

        _xSequence = new Sequence(new[]
        {
            new Keyframe(0f, 50f, Ease.Linear),
            new Keyframe(1f, 150f, Ease.Linear),
            new Keyframe(2f, 50f, Ease.Linear),
            new Keyframe(3f, 150f, Ease.Linear)
        });

        _ySequence = new Sequence(new[]
        {
            new Keyframe(0f, 150f, Ease.Linear),
            new Keyframe(1f, 50f, Ease.Linear),
            new Keyframe(2f, 150f, Ease.Linear),
            new Keyframe(3f, 50f, Ease.Linear)
        });
    }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        var x = _xSequence.Evaluate(engine.Time.ElapsedTime % 2f);
        var y = _ySequence.Evaluate(engine.Time.ElapsedTime % 2f);

        _node.Transform.Scale = new Vector2(x, y);
    }
}
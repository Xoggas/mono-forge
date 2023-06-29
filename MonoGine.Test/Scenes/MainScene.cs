using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Ecs;
using MonoGine.InputSystem;
using MonoGine.Rendering;
using MonoGine.SceneGraph.Nodes;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    public override void Update(IEngine engine)
    {
        base.Update(engine);

        IMouse mouse = engine.Input.Mouse;

        if (mouse.WasPressed(MouseButton.Right))
        {
            var square = World.CreateEntity<Square>();
            square.Node.Texture = engine.ResourceManager.Load<Texture2D>("Rect.png");
            square.Node.Shader = new Shader(engine.ResourceManager.Load<Effect>("Shaders/Brightness.fx"));
            square.Node.Transform.Position = mouse.Position;
            square.Node.Transform.Depth = engine.Time.ElapsedTime;
            square.Node.Transform.Scale = Vector2.One * 80;
            square.Node.Transform.Pivot = Vector2.Zero;
            square.Node.SetParent(Root);
        }
    }

    protected override void OnLoadResources(IEngine engine)
    {
        engine.ResourceManager.Load<Effect>("Shaders/Brightness.fx");
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}

public sealed class Square : Entity
{
    private Rectangle _boundingRectangle;
    private bool _isSelected;

    public Square()
    {
        Node = new SpriteNode();
    }

    public SpriteNode Node { get; }

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        _boundingRectangle = new Rectangle(Node.Transform.Position.ToPoint(), Node.Transform.Scale.ToPoint());
        
        if (_boundingRectangle.Contains(engine.Input.Mouse.Position))
        {
            if (engine.Input.Mouse.WasPressed(MouseButton.Left))
            {
                _isSelected = true;
            }
            
            Node.Shader?.Properties.SetFloat("Brightness", 0.1f);
        }
        else
        {
            Node.Shader?.Properties.SetFloat("Brightness", 0f);
        }

        if (!engine.Input.Mouse.IsPressed(MouseButton.Left))
        {
            _isSelected = false;
        }

        if (_isSelected)
        {
            Node.Transform.Position += engine.Input.Mouse.Delta;
        }
    }
}

using System.Collections.Generic;
using MonoGine.Rendering.Batching;

namespace MonoGine.SceneGraph;

public class Node : IObject, IDrawable, IUpdatable, IDestroyable
{
    private readonly List<Node> _children;

    public Node()
    {
        IsActive = true;
        Transform = new Transform(this);
        _children = new List<Node>();
    }

    public bool IsActive { get; set; }
    public Transform Transform { get; }
    public Node? Parent { get; private set; }
    public IReadOnlyList<Node> Children => _children;

    public virtual void Update(IEngine engine)
    {
        Transform.Update(engine);

        foreach (var child in Children)
        {
            if (child.IsActive)
            {
                child.Update(engine);
            }
        }
    }

    public virtual void Draw(IEngine engine, IBatch batch)
    {
        foreach (var child in Children)
        {
            if (child.IsActive)
            {
                child.Draw(engine, batch);
            }
        }
    }

    public void SetParent(Node? parent)
    {
        Parent?._children.Remove(this);
        Parent = parent;
        Parent?._children.Add(this);
    }

    public void SetChild(Node child)
    {
        child.SetParent(this);
    }

    public void Destroy()
    {
        SetParent(null);
        Dispose();
    }
    
    public void Dispose()
    {
        Transform.Dispose();
    }
}

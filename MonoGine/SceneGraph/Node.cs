using System.Collections.Generic;
using MonoGine.Animations;
using MonoGine.Rendering.Batching;

namespace MonoGine.SceneGraph;

public class Node : IObject, IDrawable, IUpdatable, IDestroyable, IAnimatable
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
    public IEnumerable<Node> Children => _children;

    public float this[string propertyName]
    {
        set
        {
            if (propertyName.Equals("isActive"))
            {
                IsActive = (int)value >= 1;
            }
            else
            {
                throw new KeyNotFoundException(propertyName);
            }
        }
    }

    public virtual void Update(IEngine engine)
    {
        Transform.Update(engine);

        for (var index = 0; index < _children.Count; index++)
        {
            Node child = _children[index];

            if (child.IsActive)
            {
                child.Update(engine);
            }
        }
    }

    public virtual void Draw(IEngine engine, IBatch batch)
    {
        for (var index = 0; index < _children.Count; index++)
        {
            Node child = _children[index];

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
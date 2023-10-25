using System.Collections.Generic;
using Microsoft.Xna.Framework;
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

    public string? Name { get; set; }
    public bool IsActive { get; set; }
    public Transform Transform { get; }
    public Node? Parent { get; private set; }
    public IEnumerable<Node> Children => _children;

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

    public IAnimatable? FindChildByName(string name)
    {
        return _children.Find(x => name.Equals(x.Name));
    }

    public virtual void SetProperty(string name, float value)
    {
        switch (name)
        {
            case "isActive":
                IsActive = value > 0;
                break;
            case "pos.x":
                Transform.Position = new Vector2(value, Transform.Position.Y);
                break;
            case "pos.y":
                Transform.Position = new Vector2(Transform.Position.X, value);
                break;
            case "rot.x":
                Transform.Rotation = new Vector3(value, Transform.Rotation.Y, Transform.Rotation.Z);
                break;
            case "rot.y":
                Transform.Rotation = new Vector3(Transform.Rotation.X, value, Transform.Rotation.Z);
                break;
            case "rot.z":
                Transform.Rotation = new Vector3(Transform.Rotation.X, Transform.Rotation.Y, value);
                break;
            case "scale.x":
                Transform.Scale = new Vector2(value, Transform.Scale.Y);
                break;
            case "scale.y":
                Transform.Scale = new Vector2(Transform.Scale.X, value);
                break;
            case "pivot.x":
                Transform.Pivot = new Vector2(value, Transform.Pivot.Y);
                break;
            case "pivot.y":
                Transform.Pivot = new Vector2(Transform.Pivot.X, value);
                break;
            case "skew.x":
                Transform.Skew = new Vector2(value, Transform.Skew.Y);
                break;
            case "skew.y":
                Transform.Skew = new Vector2(Transform.Skew.X, value);
                break;
            case "depth":
                Transform.Depth = value;
                break;
            default:
                throw new KeyNotFoundException(name);
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
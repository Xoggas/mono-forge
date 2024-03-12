using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoForge.Animations;
using MonoForge.Rendering.Batching;

namespace MonoForge.SceneGraph;

public class Node : IDrawable, IUpdatable, IDestroyable, IAnimatable
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

    public Node? FindChildByName(string name)
    {
        return _children.Find(x => name.Equals(x.Name));
    }

    public IAnimatable? GetChild(string name)
    {
        return FindChildByName(name);
    }

    public virtual void Update(GameBase gameBase, float deltaTime)
    {
        Transform.Update(gameBase, deltaTime);

        for (var index = 0; index < _children.Count; index++)
        {
            Node child = _children[index];

            if (child.IsActive)
            {
                child.Update(gameBase, deltaTime);
            }
        }
    }

    public virtual void Draw(GameBase gameBase, IRenderQueue renderQueue)
    {
        for (var index = 0; index < _children.Count; index++)
        {
            Node child = _children[index];

            if (child.IsActive)
            {
                child.Draw(gameBase, renderQueue);
            }
        }
    }

    public virtual Action<float> GetPropertySetter(string name)
    {
        return name switch
        {
            "isActive" => value => IsActive = value > 0,
            "pos.x" => value => Transform.Position = new Vector2(value, Transform.Position.Y),
            "pos.y" => value => Transform.Position = new Vector2(Transform.Position.X, value),
            "rot.x" => value => Transform.Rotation = new Vector3(value, Transform.Rotation.Y, Transform.Rotation.Z),
            "rot.y" => value => Transform.Rotation = new Vector3(Transform.Rotation.X, value, Transform.Rotation.Z),
            "rot.z" => value => Transform.Rotation = new Vector3(Transform.Rotation.X, Transform.Rotation.Y, value),
            "scale.x" => value => Transform.Scale = new Vector2(value, Transform.Scale.Y),
            "scale.y" => value => Transform.Scale = new Vector2(Transform.Scale.X, value),
            "pivot.x" => value => Transform.Pivot = new Vector2(value, Transform.Pivot.Y),
            "pivot.y" => value => Transform.Pivot = new Vector2(Transform.Pivot.X, value),
            "skew.x" => value => Transform.Skew = new Vector2(value, Transform.Skew.Y),
            "skew.y" => value => Transform.Skew = new Vector2(Transform.Skew.X, value),
            "depth" => value => Transform.Depth = value,
            _ => throw new KeyNotFoundException(name)
        };
    }

    public void SetParent(Node? parent)
    {
        Parent?._children.Remove(this);
        Parent = parent;
        Parent?._children.Add(this);
    }

    public void AddChild(Node child)
    {
        child.SetParent(this);
    }

    public void Destroy()
    {
        SetParent(null);
    }
}
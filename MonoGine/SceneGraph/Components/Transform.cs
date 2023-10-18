using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGine.Animations;
using MonoGine.Utilities;

namespace MonoGine.SceneGraph;

public sealed class Transform : IObject, IUpdatable, IAnimatable
{
    public Transform(Node node)
    {
        Node = node;
        Scale = Vector2.One;
        Pivot = Vector2.One * 0.5f;
    }

    public Node Node { get; }

    public Vector2 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector2 Scale { get; set; }
    public Vector2 Pivot { get; set; }
    public Vector2 Skew { get; set; }
    public float Depth { get; set; }

    public Matrix LocalMatrix { get; private set; }
    public Matrix WorldMatrix { get; private set; }
    public float WorldDepth { get; private set; }

    public float this[string propertyName]
    {
        set
        {
            switch (propertyName)
            {
                case "pos.x":
                    Position = new Vector2(value, Position.Y);
                    break;
                case "pos.y":
                    Position = new Vector2(Position.X, value);
                    break;
                case "rot.x":
                    Rotation = new Vector3(value, Rotation.Y, Rotation.Z);
                    break;
                case "rot.y":
                    Rotation = new Vector3(Rotation.X, value, Rotation.Z);
                    break;
                case "rot.z":
                    Rotation = new Vector3(Rotation.X, Rotation.Y, value);
                    break;
                case "scale.x":
                    Scale = new Vector2(value, Scale.Y);
                    break;
                case "scale.y":
                    Scale = new Vector2(Scale.X, value);
                    break;
                case "pivot.x":
                    Pivot = new Vector2(value, Pivot.Y);
                    break;
                case "pivot.y":
                    Pivot = new Vector2(Pivot.X, value);
                    break;
                case "skew.x":
                    Skew = new Vector2(value, Skew.Y);
                    break;
                case "skew.y":
                    Skew = new Vector2(Skew.X, value);
                    break;
                case "depth":
                    Depth = value;
                    break;
                default:
                    throw new KeyNotFoundException(propertyName);
            }
        }
    }

    public void Update(IEngine engine)
    {
        UpdateLocalMatrix();
        UpdateWorldMatrix();
    }

    public void Dispose()
    {
    }

    private void UpdateLocalMatrix()
    {
        LocalMatrix = MatrixUtility.CreateTrssMatrix(Position, Rotation, Scale, Skew);
    }

    private void UpdateWorldMatrix()
    {
        if (Node.Parent != null)
        {
            WorldMatrix = LocalMatrix * Node.Parent.Transform.WorldMatrix;
            WorldDepth = Depth + Node.Parent.Transform.Depth;
        }
        else
        {
            WorldMatrix = LocalMatrix;
            WorldDepth = Depth;
        }
    }
}
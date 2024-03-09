﻿using Microsoft.Xna.Framework;
using MonoGine.Utilities;

namespace MonoGine.SceneGraph;

public sealed class Transform : IObject, IUpdatable
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

    public void Update(IGame game, float deltaTime)
    {
        UpdateLocalMatrix();
        UpdateWorldMatrix();
    }

    public void Dispose()
    {
    }

    private void UpdateLocalMatrix()
    {
        LocalMatrix = MatrixUtility.CreateTRSSMatrix(Position, Rotation, Scale, Skew);
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
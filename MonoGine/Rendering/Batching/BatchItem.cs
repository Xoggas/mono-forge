using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

/// <summary>
/// Represents raw data passed to batcher.
/// </summary>
public readonly struct BatchItem : IComparable<BatchItem>, IEquatable<BatchItem>
{
    internal readonly Texture2D _texture;
    internal readonly Mesh _mesh;
    internal readonly Shader? _shader;
    private readonly float _depth;

    internal BatchItem(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _texture = texture;
        _mesh = mesh;
        _shader = shader;
        _depth = depth;
    }

    public int CompareTo(BatchItem other)
    {
        return _depth.CompareTo(other._depth);
    }

    public bool Equals(BatchItem other)
    {
        return ReferenceEquals(_texture, other._texture) ||
               ReferenceEquals(_shader, other._shader) ||
               (_shader != null && _shader.Equals(other._shader));
    }
}
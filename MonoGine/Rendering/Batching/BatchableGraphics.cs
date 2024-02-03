using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

/// <summary>
/// Represents raw data passed to batcher.
/// </summary>
internal sealed class BatchableGraphics : IEquatable<BatchableGraphics>, IComparable<BatchableGraphics>
{
    internal bool IsValid => !_isDisposed && _texture is not null && _mesh is not null;
    internal Texture2D Texture => _texture ?? throw new ArgumentNullException(nameof(_texture));
    internal Mesh Mesh => _mesh ?? throw new ArgumentNullException(nameof(_texture));
    internal Shader? Shader { get; private set; }

    private Texture2D? _texture;
    private Mesh? _mesh;
    private bool _isDisposed;
    private float _depth;

    public bool Equals(BatchableGraphics? other)
    {
        var areTextureReferencesEqual = _texture == other?._texture;
        var areShadersEqual =
            ReferenceEquals(Shader, other?.Shader) || (Shader != null && Shader.Equals(other?.Shader));
        return areTextureReferencesEqual && areShadersEqual;
    }

    public override bool Equals(object? obj)
    {
        return obj is BatchableGraphics item && Equals(item);
    }

    public override int GetHashCode()
    {
        return default;
    }

    public void Clear()
    {
        Shader = null;
        _texture = null;
        _mesh = null;
        _isDisposed = true;
    }

    public int CompareTo(BatchableGraphics? other)
    {
        return _depth.CompareTo(other?._depth);
    }

    internal void Set(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        _texture = texture;
        _mesh = mesh;
        Shader = shader;
        _depth = depth;
    }

    public static bool operator ==(BatchableGraphics left, BatchableGraphics right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(BatchableGraphics left, BatchableGraphics right)
    {
        return !(left == right);
    }
}
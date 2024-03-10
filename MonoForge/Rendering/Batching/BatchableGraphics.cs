using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering.Batching;

/// <summary>
/// Represents raw data passed to batcher.
/// </summary>
internal sealed class BatchableGraphics : IEquatable<BatchableGraphics>, IComparable<BatchableGraphics>
{
    internal bool IsInvalid => _isDisposed || Texture is null || Mesh is null;
    internal Texture2D? Texture { get; private set; }
    internal Mesh? Mesh { get; private set; }
    internal Shader? Shader { get; private set; }

    private bool _isDisposed;
    private float _depth;

    public bool Equals(BatchableGraphics? other)
    {
        var areTextureReferencesEqual = Texture == other?.Texture;
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
        Texture = null;
        Mesh = null;
        _isDisposed = true;
    }

    public int CompareTo(BatchableGraphics? other)
    {
        return _depth.CompareTo(other?._depth);
    }

    internal void Set(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        Texture = texture;
        Mesh = mesh;
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
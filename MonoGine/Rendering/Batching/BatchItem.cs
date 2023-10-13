using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

public readonly struct BatchItem : IComparable<BatchItem>, IEquatable<BatchItem>
{
    internal readonly Texture2D _texture;
    internal readonly Shader? _shader;
    internal readonly VertexPositionColorTexture _topLeft;
    internal readonly VertexPositionColorTexture _topRight;
    internal readonly VertexPositionColorTexture _bottomLeft;
    internal readonly VertexPositionColorTexture _bottomRight;
    private readonly float _depth;

    internal BatchItem(Texture2D texture, Shader? shader, Color color, Matrix matrix, Vector2 pivot,
        Rectangle textureRect, float depth)
    {
        _texture = texture;
        _shader = shader;
        
        _topLeft = new VertexPositionColorTexture(
            Vector3.Transform(new Vector3(0f - pivot.X, 0f - pivot.Y, 0f), matrix), color,
            new Vector2(textureRect.X, textureRect.Y));
        
        _topRight = new VertexPositionColorTexture(
            Vector3.Transform(new Vector3(1f - pivot.X, 0f - pivot.Y, 0f), matrix), color,
            new Vector2(textureRect.X + textureRect.Width, textureRect.Y));
        
        _bottomLeft = new VertexPositionColorTexture(
            Vector3.Transform(new Vector3(0f - pivot.X, 1f - pivot.Y, 0f), matrix), color,
            new Vector2(textureRect.X, textureRect.Y + textureRect.Height));
        
        _bottomRight = new VertexPositionColorTexture(
            Vector3.Transform(new Vector3(1f - pivot.X, 1f - pivot.Y, 0f), matrix), color,
            new Vector2(textureRect.X + textureRect.Width, textureRect.Y + textureRect.Height));
        
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
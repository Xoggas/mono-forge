using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class Sprite : IDisposable
{
    private readonly Texture2D _texture;

    internal Sprite(int id, string name, Texture2D texture, Rectangle? bounds = null)
    {
        Id = id;
        Name = name;
        Bounds = bounds ?? new Rectangle(0, 0, texture.Width, texture.Height);
        _texture = texture;
    }

    public int Id { get; }
    public string Name { get; }
    public int Width => _texture.Width;
    public int Height => _texture.Height;
    public Rectangle Bounds { get; }

    public static implicit operator Texture2D(Sprite sprite)
    {
        return sprite._texture;
    }

    public void Dispose()
    {
        _texture.Dispose();
    }
}
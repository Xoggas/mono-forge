using Microsoft.Xna.Framework.Graphics;
using MonoGine.ResourceLoading;

namespace MonoGine.Rendering;

public sealed class Sprite : IResource
{
    public int Width => _texture.Width;
    public int Height => _texture.Height;

    private readonly Texture2D _texture;

    public Sprite(Texture2D texture)
    {
        _texture = texture;
    }

    public static implicit operator Texture2D(Sprite sprite)
    {
        return sprite._texture;
    }

    public void Dispose()
    {
        _texture.Dispose();
    }
}
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

internal sealed class SpriteReader : IAssetReader<Sprite>
{
    public Sprite Read(IEngine engine, string path)
    {
        Texture2D texture = Texture2D.FromFile(engine.GraphicsDevice, PathUtils.GetAbsolutePath(path));
        texture.Name = Path.GetFileNameWithoutExtension(path);
        return new Sprite(texture);
    }
}
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

internal sealed class SpriteReader : IAssetReader<Sprite>, IAssetWriter<Sprite>
{
    public Sprite Read(IEngine engine, string localPath)
    {
        var absolutePath = PathUtility.GetAbsoluteAssetPath(localPath);
        Texture2D texture = Texture2D.FromFile(engine.GraphicsDevice, absolutePath);
        texture.Name = Path.GetFileNameWithoutExtension(localPath);
        return new Sprite(texture);
    }

    public void Write(IEngine engine, string localPath, Sprite resource)
    {
        var absolutePath = PathUtility.GetAbsoluteAssetPath(localPath);
        using FileStream writeStream = File.OpenWrite(absolutePath);
        var texture = (Texture2D)resource;
        texture.SaveAsPng(writeStream, texture.Width, texture.Height);
    }
}
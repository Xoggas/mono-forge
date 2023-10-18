using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.ResourceLoading;

internal sealed class SpriteResourceProcessor : IResourceReader<Sprite>, IResourceWriter<Sprite>
{
    public Sprite Read(IEngine engine, string path)
    {
        try
        {
            return new Sprite(Texture2D.FromFile(engine.GraphicsDevice, PathUtils.GetAbsolutePath(path)));
        }
        catch
        {
            throw new FileProcessingErrorException($"An error occured when loading texture from path {path}");
        }
    }

    public void Write(IEngine engine, string path, Sprite sprite)
    {
        using FileStream stream = File.OpenWrite(PathUtils.GetAbsolutePath(path));

        var texture = (Texture2D)sprite;
        var extension = PathUtils.GetExtension(path);

        if (ExtensionEquals(extension, "jpg"))
        {
            texture.SaveAsJpeg(stream, texture.Width, texture.Width);
        }
        else if (ExtensionEquals(extension, "png"))
        {
            texture.SaveAsPng(stream, texture.Width, texture.Width);
        }
    }

    private bool ExtensionEquals(string a, string b)
    {
        return a.Equals(b, StringComparison.OrdinalIgnoreCase);
    }
}
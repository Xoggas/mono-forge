using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Resources;

internal sealed class Texture2DProcessor : IProcessor<Texture2D>
{
    public Texture2D Load(IEngine engine, string path)
    {
        try
        {
            return Texture2D.FromFile(engine.GraphicsDevice, PathUtils.GetAbsolutePath(path));
        }
        catch
        {
            throw new FileProcessingErrorException($"An error occured when loading texture from path {path}");
        }
    }

    public void Save(IEngine engine, string path, Texture2D texture)
    {
        using var stream = File.OpenWrite(PathUtils.GetAbsolutePath(path));
        
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

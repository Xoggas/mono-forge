using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace MonoGine.Resources;

internal sealed class Texture2DProcessor : IProcessor
{
    public T? Load<T>(IEngine engine, string path) where T : class
    {
        try
        {
            return Texture2D.FromFile(engine.GraphicsDevice, PathUtils.GetAbsolutePath(path)) as T;
        }
        catch
        {
            return null;
        }
    }

    public void Save<T>(IEngine engine, string path, T? resource) where T : class
    {
        if (resource == null || resource is not T)
        {
            return;
        }

        try
        {
            using (var stream = File.OpenWrite(PathUtils.GetAbsolutePath(path)))
            {
                Texture2D? texture = resource as Texture2D;

                var extension = PathUtils.GetExtension(path);

                if (ExtensionEquals(extension, "jpg"))
                {
                    texture?.SaveAsJpeg(stream, texture.Width, texture.Width);
                }
                else if(ExtensionEquals(extension, "png"))
                {
                    texture?.SaveAsPng(stream, texture.Width, texture.Width);
                }
            }
        }
        catch
        {

        }
    }

    private bool ExtensionEquals(string a, string b)
    {
        return a.Equals(b, StringComparison.OrdinalIgnoreCase);
    }
}

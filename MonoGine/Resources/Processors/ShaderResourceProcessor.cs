using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.ResourceLoading;

internal sealed class ShaderResourceProcessor : IResourceReader<Shader>
{
    public Shader Read(IEngine engine, string path)
    {
        var bytes = File.ReadAllBytes(PathUtils.GetAbsolutePath(path));
        return new Shader(new Effect(engine.GraphicsDevice, bytes));
    }
}
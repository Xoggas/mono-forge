using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

internal sealed class ShaderReader : IAssetReader<Shader>
{
    public Shader Read(IEngine engine, string path)
    {
        var bytes = File.ReadAllBytes(PathUtils.GetAbsolutePath(path));
        return new Shader(new Effect(engine.GraphicsDevice, bytes));
    }
}
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

internal sealed class ShaderReader : IAssetReader<Shader>
{
    public Shader Read(IEngine engine, string localPath)
    {
        var absolutePath = PathUtility.GetAbsoluteAssetPath(localPath);
        var bytes = File.ReadAllBytes(absolutePath);
        return new Shader(new Effect(engine.GraphicsDevice, bytes));
    }
}
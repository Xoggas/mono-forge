using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.ResourceLoading;

internal sealed class EffectProcessor : IProcessor<Effect>
{
    public Effect Load(IEngine engine, string path)
    {
        var bytes = File.ReadAllBytes(PathUtils.GetAbsolutePath(path));
        return new Effect(engine.GraphicsDevice, bytes);
    }

    public void Save(IEngine engine, string path, Effect resource)
    {
        throw new InvalidOperationException("Shader saving is not supported!");
    }
}
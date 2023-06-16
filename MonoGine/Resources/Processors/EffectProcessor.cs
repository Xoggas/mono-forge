using MGFXC;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoGine.Resources;

internal sealed class EffectProcessor : IProcessor
{
    public T? Load<T>(IEngine engine, string path) where T : class
    {
        try
        {
            return new Effect(engine.GraphicsDevice, ShaderCompiler.Compile(PathUtils.GetAbsolutePath(path))) as T;
        }
        catch
        {
            return null;
        }
    }

    public void Save<T>(IEngine engine, string path, T? resource) where T : class
    {
        throw new Exception("Shader saving is not supported!");
    }
}

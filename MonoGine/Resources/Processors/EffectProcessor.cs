using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Effect.Compiler;

namespace MonoGine.Resources;

internal sealed class EffectProcessor : IProcessor<Effect>
{
    public Effect Load(IEngine engine, string path)
    {
        var bytes = ShaderCompiler.Compile(PathUtils.GetAbsolutePath(path));

        if (bytes != null)
        {
            return new Effect(engine.GraphicsDevice, bytes);
        }

        throw new FileProcessingErrorException("Shader compilation error!");
    }

    public void Save(IEngine engine, string path, Effect resource)
    {
        throw new InvalidOperationException("Shader saving is not supported!");
    }
}

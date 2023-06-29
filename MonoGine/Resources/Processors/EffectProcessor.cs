using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Effect.Compiler;

namespace MonoGine.Resources;

internal sealed class EffectProcessor : IProcessor
{
    public T Load<T>(IEngine engine, string path) where T : class
    {
        var bytes = ShaderCompiler.Compile(PathUtils.GetAbsolutePath(path));

        if (bytes != null)
        {
            return new Effect(engine.GraphicsDevice, bytes) as T ?? throw new InvalidCastException();
        }

        throw new FileProcessingErrorException("Shader compilation error!");
    }

    public void Save<T>(IEngine engine, string path, T? resource) where T : class
    {
        throw new InvalidOperationException("Shader saving is not supported!");
    }
}

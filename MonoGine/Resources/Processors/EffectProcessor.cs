using Microsoft.Xna.Framework.Graphics;
using MonoGame.Effect.Compiler;
using System;

namespace MonoGine.Resources;

internal sealed class EffectProcessor : IProcessor
{
    public T? Load<T>(IEngine engine, string path) where T : class
    {
        try
        {
            CompileShader(path, out var bytes);

            if (bytes != null)
            {
                return new Effect(engine.GraphicsDevice, bytes) as T;
            }
            else
            {
                throw new Exception("Shader compilation error!");
            }
        }
        catch
        {
            throw;
        }
    }

    private static void CompileShader(string path, out byte[]? bytes)
    {
        try
        {
            bytes = ShaderCompiler.Compile(PathUtils.GetAbsolutePath(path));
        }
        catch
        {
            throw;
        }
    }

    public void Save<T>(IEngine engine, string path, T? resource) where T : class
    {
        throw new Exception("Shader saving is not supported!");
    }
}

using MGFXC.Effect;
using System;
using System.Diagnostics;
using System.IO;

namespace MGFXC;

public sealed class ShaderCompiler : IDisposable
{
    private class EffectCompilerOutput : IEffectCompilerOutput
    {
        public void WriteWarning(string file, int line, int column, string message)
        {
            Debug.Print("Warning: {0}({1},{2}): {3}", file, line, column, message);
        }

        public void WriteError(string file, int line, int column, string message)
        {
            throw new Exception($"Error: {file}({line},{column}): {message}");
        }
    }

    private EffectCompilerOutput _output = new EffectCompilerOutput();

    public byte[] Compile(string path)
    {
        Options options = new Options() { SourceFile = path };

        if (File.Exists(path))
        {
            return GetShaderBytecode(options);
        }
        else
        {
            throw new Exception($"File {path} doesn't exist!");
        }
    }

    public void Dispose()
    {
        _output = null;
    }

    private byte[] GetShaderBytecode(Options options)
    {
        try
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    CompileShader(options).Write(writer, options);

                    return stream.ToArray();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private EffectObject CompileShader(Options options)
    {
        try
        {
            return EffectObject.CompileEffect(GetShaderResult(options), out _);
        }
        catch
        {
            throw;
        }
    }

    private ShaderResult GetShaderResult(Options options)
    {
        try
        {
            return ShaderResult.FromFile(options.SourceFile, options, _output);
        }
        catch
        {
            throw;
        }
    }
}
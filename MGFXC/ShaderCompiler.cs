using System;
using System.Diagnostics;
using System.IO;


namespace MonoGame.Effect.Compiler
{
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

        private static EffectCompilerOutput s_output = new EffectCompilerOutput();

        public static byte[] Compile(string path)
        {
            Options options = new Options() { SourceFile = path };

            if (File.Exists(path))
            {
                return GetShaderBytecode(options);
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            s_output = null;
        }

        private static byte[] GetShaderBytecode(Options options)
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

        private static EffectObject CompileShader(Options options)
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

        private static ShaderResult GetShaderResult(Options options)
        {
            try
            {
                return ShaderResult.FromFile(options.SourceFile, options, s_output);
            }
            catch
            {
                throw;
            }
        }
    } 
}
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGine.ResourceLoading;

internal static class PathUtils
{
    internal static IEnumerable<string> GetFiles(string path)
    {
        return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                        .Select(path => FormatPath(path));
    }

    internal static string ChangeExtension(string path, string extension)
    {
        return Path.ChangeExtension(path, extension);
    }

    internal static string GetExtension(string path)
    {
        return Path.GetExtension(path)[1..];
    }

    internal static bool Exists(string path)
    {
        return File.Exists(path);
    }

    internal static string FormatExtension(string extension)
    {
        if (extension.StartsWith('.'))
        {
            extension = extension[1..];
        }

        return extension;
    }

    internal static string FormatPath(string path)
    {
        if (path.Contains("\\"))
        {
            path = path.Replace("\\", "/");
        }

        if (path.StartsWith('/'))
        {
            path = path[1..];
        }

        return path;
    }

    internal static string GetLocalPath(string relativePath, string absolutePath)
    {
        string outputPath = Path.GetRelativePath(relativePath, absolutePath);

        outputPath = FormatPath(outputPath);

        if (Path.HasExtension(outputPath))
        {
            outputPath = outputPath.Split('.')[0];
        }

        return outputPath;
    }

    internal static string GetOrCreateAssetsFolder()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }
}

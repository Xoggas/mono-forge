using System.IO;

namespace MonoGine;

internal static class PathUtility
{
    internal static string AssetsPath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

    internal static string GetAbsoluteAssetPath(string localPath)
    {
        return Path.Combine(AssetsPath, localPath);
    }

    internal static string GetExtension(string path)
    {
        return Path.GetExtension(path)[1..];
    }
}
using System.IO;

namespace MonoGine.AssetLoading;

public static class PathUtils
{
    public static string AssetsPath { get; } = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

    public static string GetAbsolutePath(string localPath)
    {
        return Path.Combine(AssetsPath, localPath);
    }

    public static string GetExtension(string path)
    {
        return Path.GetExtension(path)[1..];
    }
}
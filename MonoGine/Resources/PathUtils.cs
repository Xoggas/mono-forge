using System.IO;

namespace MonoGine.Resources;

public static class PathUtils
{
    private static readonly string s_assetsPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

    static PathUtils()
    {
        Directory.CreateDirectory(s_assetsPath);
    }

    /// <summary>
    /// Returns assets folder path
    /// </summary>
    /// <returns></returns>
    public static string GetAssetsPath()
    {
        return s_assetsPath;
    }

    /// <summary>
    /// Gets directory of asset and creates it if needed
    /// </summary>
    /// <param name="absolutePath">An absolute path to the asset</param>
    public static void CreateDirectoryForAsset(string absolutePath)
    {
        var directoryPath = Path.GetDirectoryName(absolutePath);

        if (Directory.Exists(directoryPath))
        {
            return;
        }
        else if (directoryPath != null)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    /// <summary>
    /// Returns 
    /// </summary>
    /// <param name="localPath"></param>
    /// <returns></returns>
    public static string GetAbsolutePath(string localPath)
    {
        return Path.Combine(s_assetsPath, localPath);
    }

    /// <summary>
    /// Returns true if file exists (local path is converted to absolute path relatively to assets folder)
    /// </summary>
    /// <param name="localPath"></param>
    /// <returns></returns>
    public static bool Exists(string localPath)
    {
        return File.Exists(GetAbsolutePath(localPath));
    }

    /// <summary>
    /// Throws an exception if the asset doesn't exist.
    /// </summary>
    /// <param name="localPath"></param>
    /// <exception cref="FileNotFoundException"></exception>
    public static void ValidatePath(string localPath)
    {
        if (!File.Exists(GetAbsolutePath(localPath)))
        {
            throw new FileNotFoundException($"The file {localPath} doesn't exist!");
        }
    }

    /// <summary>
    /// Gets extension from path without leading period
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetExtension(string path)
    {
        return Path.GetExtension(path)[1..];
    }
}

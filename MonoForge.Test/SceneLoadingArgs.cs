using MonoForge.SceneManagement.Interfaces;

namespace MonoForge.Test;

public sealed class SceneLoadingArgs : ISceneLoadingArgs
{
    public static SceneLoadingArgs Empty { get; } = new();

    private SceneLoadingArgs()
    {
    }
}
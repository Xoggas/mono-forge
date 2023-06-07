namespace MonoGine.ResourceLoading;

internal sealed class Serializer : Object
{
    private string _assetFolderPath;

    internal Serializer(string assetFolderPath)
    {
        _assetFolderPath = assetFolderPath;
    }

    public override void Dispose()
    {
        _assetFolderPath = null;
    }
}

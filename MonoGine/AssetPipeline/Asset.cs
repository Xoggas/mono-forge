namespace MonoGine.AssetPipeline;

public abstract class Asset : Object
{
    private string _assetPath;
    private string _assetExtension;
    private Metadata _metadata;

    public Asset(string assetPath, string assetExtension)
    {
        _assetPath = assetPath;
        _assetExtension = assetExtension;
        _metadata = new Metadata(assetPath);
    }

    public override void Dispose()
    {
        _metadata.Dispose();
    }
}

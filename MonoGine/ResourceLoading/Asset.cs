namespace MonoGine.ResourceLoading;

public abstract class Asset : Object
{
    public Asset(Metadata metadata)
    {
        Metadata = metadata;
    }

    protected Metadata Metadata { get; private set; }

    public override void Dispose()
    {
        Metadata = null;
    }
}
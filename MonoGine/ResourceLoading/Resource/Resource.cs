namespace MonoGine.ResourceLoading;

public abstract class Resource : Object
{
    public Resource(Metadata metadata)
    {
        Metadata = metadata;
    }

    public Metadata Metadata { get; private set; }

    public override void Dispose()
    {
        Metadata = null;
    }
}
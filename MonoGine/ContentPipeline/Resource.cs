namespace MonoGine.ContentPipeline;

public abstract class Resource : Object
{
    public Resource(Metadata metadata)
    {
        Metadata = metadata;
    }

    protected Metadata Metadata { get; private set; }

    public override void Dispose()
    {
        Metadata = null;
    }
}
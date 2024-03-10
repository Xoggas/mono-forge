namespace MonoForge.SceneGraph;

public sealed class Collider : IObject
{
    public Collider(Node node)
    {
        Node = node;
    }

    public Node Node { get; }

    public void Dispose()
    {
    }
}

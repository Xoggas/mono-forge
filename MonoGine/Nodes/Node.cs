using System.Collections.Generic;

namespace MonoGine.Nodes;

public abstract class Node : Object
{
    private string _name;
    private int _order;
    private List<Node> _children;

    public Node(string name = null)
    {
        _name = name;
    }

    public Node this[int index]
    {
        get
        {
            return _children[index];
        }
    }

    public int Count => _children.Count;
    public int Order => _order; 
    public string Name => _name;    

    public void SetName(string name)
    {
        _name = name;
    }

    public void AddNode(Node node)
    {

    }

    public void RemoveNode(Node node)
    {

    }

    public void SetOrder(int order)
    {

    }

    public override void Dispose()
    {

    }
}

using MonoGine.SceneGraph;

namespace MonoGine.TextRendering;

public sealed class TextNode : Node
{
    public Font? Font { get; set; }
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }
}
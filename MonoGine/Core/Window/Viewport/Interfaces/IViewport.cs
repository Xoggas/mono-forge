namespace MonoGine;

public interface IViewport : IObject
{
    public RenderTarget Target { get; }
    public IViewportScaler Scaler { get; set; }
    public int Width { get; }
    public int Height { get; }
}

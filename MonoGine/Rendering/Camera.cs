using Microsoft.Xna.Framework;

namespace MonoGine.Rendering;

public sealed class Camera : Object
{
    private static Camera s_instance;

    private Matrix _matrix;
    private Color _backgroundColor;

    public Camera()
    {
        s_instance = this;
    }

    public static Matrix Matrix => s_instance._matrix;
    public static Color BackgroundColor
    {
        get => s_instance._backgroundColor;
        set => s_instance._backgroundColor = value;
    }

    public void Update()
    {

    }

    public override void Dispose()
    {
        s_instance = null;
    }
}

using Microsoft.Xna.Framework;

namespace MonoGine.Rendering;

public sealed class Camera : Object
{
    private static Camera s_instance;

    public Camera()
    {
        s_instance = this;
    }

    public static Matrix Matrix { get; private set; }
    public static Color BackgroundColor { get; set; }

    public void Update()
    {

    }

    public override void Dispose()
    {
        s_instance = null;
    }
}

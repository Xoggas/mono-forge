using Microsoft.Xna.Framework;
using MonoGine.Animations;

namespace MonoGine.Rendering;

public interface ICamera : IObject, IUpdatable, IAnimatable
{
    /// <summary>
    /// Gets or sets the camera position,
    /// </summary>
    public Vector2 Position { get; set; }
    
    /// <summary>
    /// Gets or sets the camera rotation.
    /// </summary>
    public float Rotation { get; set; }
    
    /// <summary>
    /// Gets or sets the camera zoom.
    /// </summary>
    public float Zoom { get; set; }
    
    /// <summary>
    /// Gets or sets the camera background color.
    /// </summary>
    public Color BackgroundColor { get; set; }
    
    /// <summary>
    /// Gets the camera transform matrix.
    /// </summary>
    public Matrix TransformMatrix { get; }
}

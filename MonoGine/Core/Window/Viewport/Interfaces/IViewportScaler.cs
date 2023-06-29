using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine;

public interface IViewportScaler
{
    public void Rescale(GraphicsDevice graphicsDevice, IViewport viewport, Point windowResolution);
}

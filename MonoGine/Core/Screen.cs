using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MonoGine;

public sealed class Screen
{
    internal Core _core;

    internal Screen(Core core)
    {
        _core = core;
    }

    public Point[] Resolutions
    {
        get => GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Select(x => new Point(x.Width, x.Height)).ToArray();
    }

    public Point Resolution
    {
        get
        {
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            return new Point(displayMode.Width, displayMode.Height);
        }
    }
}

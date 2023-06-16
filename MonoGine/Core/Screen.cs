using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace MonoGine;

/// <summary>
/// Represents a screen in the game engine.
/// </summary>
public sealed class Screen
{
    internal Core _core;

    internal Screen(Core core)
    {
        _core = core;
    }

    /// <summary>
    /// Gets an array of all available screen resolutions.
    /// </summary>
    public Point[] Resolutions
    {
        get => GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Select(x => new Point(x.Width, x.Height)).ToArray();
    }

    /// <summary>
    /// Gets the current screen resolution.
    /// </summary>
    public Point Resolution
    {
        get
        {
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            return new Point(displayMode.Width, displayMode.Height);
        }
    }
}
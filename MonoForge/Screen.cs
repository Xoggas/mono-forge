using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge;

/// <summary>
/// Represents a screen in the game engine.
/// </summary>
public sealed class Screen
{
    /// <summary>
    /// Gets an array of all available screen resolutions.
    /// </summary>
    public static IEnumerable<Point> Resolutions
    {
        get
        {
            DisplayModeCollection supportedModes = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
            return supportedModes.Select(x => new Point(x.Width, x.Height));
        }
    }

    /// <summary>
    /// Gets the current screen resolution.
    /// </summary>
    public static Point Resolution
    {
        get
        {
            DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            return new Point(displayMode.Width, displayMode.Height);
        }
    }
}
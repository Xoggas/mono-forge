using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MonoGine.InputSystem;

public sealed class Input : ISystem
{
    internal Input()
    {
        Keyboard = new Keyboard();
        Mouse = new Mouse();
        Gamepads = new List<Gamepad>
        {
            new Gamepad(PlayerIndex.One),
            new Gamepad(PlayerIndex.Two),
            new Gamepad(PlayerIndex.Three),
            new Gamepad(PlayerIndex.Four),
        };
    }

    public static Keyboard Keyboard { get; private set; }
    public static Mouse Mouse { get; private set; }
    public static IReadOnlyList<Gamepad> Gamepads { get; private set; }

    public void Dispose()
    {
        Keyboard = null;
        Mouse = null;
        Gamepads = null;
    }

    public void Initialize()
    {

    }

    public void PreUpdate()
    {

    }

    public void PostUpdate()
    {
        Keyboard.Update();
        Mouse.Update();

        for (int i = 0; i < Gamepads.Count; i++)
        {
            Gamepads[i].Update();
        }
    }
}

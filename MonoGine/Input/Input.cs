using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MonoGine.InputSystem;

public sealed class Input : System
{
    public event Action<InputDevice> OnDeviceStateChanged; 

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

    public override void Dispose()
    {
        Keyboard = null;
        Mouse = null;
        Gamepads = null;
    }

    internal override void Initialize()
    {

    }

    internal override void PreUpdate()
    {

    }

    internal override void PostUpdate()
    {
        Keyboard.Update();
        Mouse.Update();

        for (int i = 0; i < Gamepads.Count; i++)
        {
            Gamepads[i].Update();
        }
    }
}

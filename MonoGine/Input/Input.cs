using Microsoft.Xna.Framework;

namespace MonoGine.InputSystem;

public sealed class Input : System
{
    public Input()
    {
        Keyboard = new Keyboard();
        Mouse = new Mouse();
        Gamepads = new Gamepad[]
        {
            new Gamepad(PlayerIndex.One),
            new Gamepad(PlayerIndex.Two),
            new Gamepad(PlayerIndex.Three),
            new Gamepad(PlayerIndex.Four),
        };
    }

    public static Keyboard Keyboard { get; private set; }
    public static Mouse Mouse { get; private set; }
    public static Gamepad[] Gamepads { get; private set; }

    public override void Initialize()
    {

    }

    public override void PreUpdate()
    {

    }

    public override void PostUpdate()
    {
        Keyboard.Update();
        Mouse.Update();

        for (int i = 0; i < Gamepads.Length; i++)
        {
            Gamepads[i].Update();
        }
    }

    public override void Dispose()
    {
        Keyboard = null;
        Mouse = null;
        Gamepads = null;
    }
}

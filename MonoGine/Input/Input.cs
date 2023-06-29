using Microsoft.Xna.Framework;

namespace MonoGine.InputSystem;

public sealed class Input : IInput
{
    internal Input()
    {
        Keyboard = new Keyboard();
        Mouse = new Mouse();
        Gamepads = new IGamepad[4]
        {
            new Gamepad(PlayerIndex.One),
            new Gamepad(PlayerIndex.Two),
            new Gamepad(PlayerIndex.Three),
            new Gamepad(PlayerIndex.Four),
        };
    }

    public IKeyboard Keyboard { get; }
    public IMouse Mouse { get; }
    public IGamepad[] Gamepads { get; }

    public void Update(IEngine engine)
    {
        Keyboard.Update(engine);
        Mouse.Update(engine);

        for (int i = 0; i < 4; i++)
        {
            Gamepads[i].Update(engine);
        }
    }

    public void Dispose()
    {
        Keyboard.Dispose();
    }
}

using Microsoft.Xna.Framework;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    public override void OnInitialize()
    {
        base.OnInitialize();

        Window.Title = "MonoGine";
        Window.Resolution = new Point(1280, 720);
    }
}
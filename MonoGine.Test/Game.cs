using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        Window.Title = "MonoGine";
        Window.Resolution = new Point(1280, 720);

        Cursor.IsVisible = true;
        Cursor.Texture = ResourceManager.Load<Texture2D>("Cursor.png");
    }
}
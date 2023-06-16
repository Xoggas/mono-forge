using Microsoft.Xna.Framework;
using MonoGine.Ecs;
using MonoGine.Test.Scenes;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnInitialize()
    {
        base.OnInitialize();

        Window.Title = "MonoGine";
        Window.Resolution = new Point(1280, 720);

        SceneManager.Load<MainScene>(this);
    }
}

public sealed class Dummy : Entity
{

}
using Microsoft.Xna.Framework;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnStart()
    {
        Window.Title = "AAAAA";
        Window.Resolution = new Point(1280, 720);
        Window.IsFixedFramerate = true; 
        Window.Framerate = 60;
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnQuit()
    {
        
    }
}
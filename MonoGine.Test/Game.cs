using Microsoft.Xna.Framework;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class Game : Engine
{
    protected override void OnPreInitialize()
    {
        Window.Title = "MonoGine";
        Window.Resolution = new Point(1280, 720);
        Window.IsFixedFramerate = true; 
        Window.Framerate = 60;
        Window.IsFullscreen = false;
    }

    protected override void OnPostInitialize()
    {
        SceneManager.Load<MainScene>();
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnQuit()
    {
        
    }
}
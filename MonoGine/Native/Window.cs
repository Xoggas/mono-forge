using Microsoft.Xna.Framework;

namespace MonoGine;

public class Window
{
    private static GameWindow s_window;

    public Window(GameWindow gameWindow)
    {
        s_window = gameWindow;
    }

    public void SetTitle(string title)
    {
        s_window.Title = title;
    }

    public void SetFullscreen(bool fullscreen)
    {

    }
}

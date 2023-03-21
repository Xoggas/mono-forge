using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.SceneManagement;
using System.Collections.Generic;

namespace MonoGine;

public class Engine : Game
{
    private static List<System> s_systems;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Engine()
    {
        s_systems = new List<System>();
    }

    public static void AddSystem(System system)
    {
        s_systems.Add(system);
    }

    public static bool TryGetSystem<T>(out T system) where T : System
    {
        system = (T)s_systems.Find(x => x.GetType() == typeof(T));
        return system != null;
    }

    public static void DisposeSystem<T>() where T : System
    {
        if (TryGetSystem(out T system))
        {
            s_systems.Remove(system);
            system.Dispose();
        }
    }

    protected override void BeginRun()
    {
        base.BeginRun();

        AddSystem(new SceneManager());
    }

    protected override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < s_systems.Count; i++)
        {
            s_systems[i].Initialize();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        for(int i = 0; i < s_systems.Count; i++)
        {
            s_systems[i].Update();
        }
    }
}
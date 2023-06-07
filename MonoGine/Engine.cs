using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Input;
using MonoGine.Rendering;
using MonoGine.ResourceLoading;
using MonoGine.SceneManagement;
using System.Collections.Generic;

namespace MonoGine;

public class Engine : Game
{
    private List<System> _systems;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Engine()
    {
        _systems = new List<System>();
    }

    public void AddSystem(System system)
    {
        _systems.Add(system);
    }

    protected override void BeginRun()
    {
        base.BeginRun();

        AddSystem(new Resources());
        AddSystem(new InputProvider());
        AddSystem(new Batcher(_spriteBatch));
        AddSystem(new SceneManager());
    }

    protected override void Initialize()
    {
        base.Initialize();

        for (int i = 0; i < _systems.Count; i++)
        {
            _systems[i].Initialize();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        for (int i = 0; i < _systems.Count; i++)
        {
            _systems[i].PreUpdate();
        }

        base.Update(gameTime);

        for(int i = 0; i < _systems.Count; i++)
        {
            _systems[i].PostUpdate();
        }
    }
}
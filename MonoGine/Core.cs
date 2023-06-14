using Microsoft.Xna.Framework;
using System;

namespace MonoGine;

internal sealed class Core : Game
{
    internal event Action? OnInitialize;
    internal event Action? OnLoadResources;
    internal event Action<GameTime>? OnBeginUpdate;
    internal event Action<GameTime>? OnUpdate;
    internal event Action<GameTime>? OnBeginDraw;
    internal event Action<GameTime>? OnDraw;

    internal Core()
    {
        GraphicsDeviceManager = new GraphicsDeviceManager(this);
    }

    internal GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

    protected override void Initialize()
    {
        base.Initialize();

        OnInitialize?.Invoke();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        OnLoadResources?.Invoke();
    }

    protected override void Update(GameTime gameTime)
    {
        OnBeginUpdate?.Invoke(gameTime);

        base.Update(gameTime);

        OnUpdate?.Invoke(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        OnBeginDraw?.Invoke(gameTime);

        base.Draw(gameTime);

        OnDraw?.Invoke(gameTime);
    }
}
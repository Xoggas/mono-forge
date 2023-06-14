using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class Time : IObject
{
    internal Time()
    {

    }

    public float Speed { get; set; }
    public float ElapsedTime { get; private set; }
    public float DeltaTime { get; private set; }

    public void Update(GameTime gameTime)
    {
        ElapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;
        DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * Speed;
    }

    public void Dispose()
    {

    }
}

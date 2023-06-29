using Microsoft.Xna.Framework;

namespace MonoGine;

public sealed class Time : IObject
{
    internal Time()
    {

    }

    /// <summary>
    /// Only affects on delta time value, doesn't change game speed
    /// </summary>
    public float Speed { get; set; } = 1f;

    /// <summary>
    /// Total elapsed time from the game launch
    /// </summary>
    public float ElapsedTime { get; private set; }

    /// <summary>
    /// Time elapsed from the last frame
    /// </summary>
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

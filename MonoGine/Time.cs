using Microsoft.Xna.Framework;

namespace MonoGine;

public static class Time
{
    private static float s_lastElapsedTime;

    public static float Speed { get; set; }
    public static float ElapsedTime { get; private set; }
    public static float DeltaTime { get; private set; }

    public static void Update(GameTime gameTime)
    {
        ElapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        DeltaTime = ElapsedTime - s_lastElapsedTime;
        s_lastElapsedTime = ElapsedTime;
    }
}

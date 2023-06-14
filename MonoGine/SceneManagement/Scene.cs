using Microsoft.Xna.Framework;
using MonoGine.Graphics;
using EcsWorld = MonoGine.Ecs.World;
using PhysicsWorld = Genbox.VelcroPhysics.Dynamics.World;

namespace MonoGine.SceneManagement;

public abstract class Scene : IScene
{
    public EcsWorld? World { get; private set; }
    public PhysicsWorld? Physics { get; private set; }
    public Camera? Camera { get; private set; }

    public void Load(object[] args)
    {
        World = new EcsWorld();
        Physics = new PhysicsWorld(Vector2.Zero);
        Camera = new Camera();
    }

    public void Unload()
    {

    }

    public void Update(Engine engine)
    {

    }

    public void Dispose()
    {
        World?.Dispose();
        Physics?.Clear();
        Camera?.Dispose();
    }
}
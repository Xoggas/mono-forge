using MonoGine.Graphics;
using EcsWorld = MonoGine.Ecs.World;
using PhysicsWorld = Genbox.VelcroPhysics.Dynamics.World;

namespace MonoGine.SceneManagement;

public interface IScene : IUpdatable, IObject
{
    public EcsWorld? World { get; }
    public PhysicsWorld? Physics { get; }
    public Camera? Camera { get; }

    public void Load(object[] args);
    public void Unload();
}

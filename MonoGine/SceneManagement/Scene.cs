using MonoGine.Graphics;

namespace MonoGine.SceneManagement;

public abstract class Scene : IScene
{
    public Ecs.World World => null; 
    public Genbox.VelcroPhysics.Dynamics.World Physics => null;
    public Camera Camera => null;

    public void Load(object[] args)
    {

    }

    public void Unload()
    {

    }

    public void Update(Engine engine)
    {
        
    }

    public void Dispose()
    {
        
    }
}
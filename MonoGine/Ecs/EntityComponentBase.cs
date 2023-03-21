namespace MonoGine.Ecs;

public abstract class EntityComponentBase : Object
{
    public bool Enabled { get; private set; } = true;
    public bool Started { get; private set; }
    public bool Destroyed { get; private set; }

    public virtual void SetActive(bool state)
    {
        if (Enabled == state)
        {
            return;
        }

        Enabled = state;
    }

    public virtual void Start()
    {
        if (Started)
        {
            return;
        }

        Started = true;
    }

    public virtual void Update()
    {
        if (!Started)
        {
            Start();
            return;
        }
    }

    public virtual void Destroy()
    {
        Enabled = false;
        Destroyed = true;
    }
}

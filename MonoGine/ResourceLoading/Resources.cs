using System.Threading.Tasks;

namespace MonoGine.ResourceLoading;

public sealed class Resources : System
{
    private static Serializer s_serializer;
    private static Cache s_cache;

    internal Resources()
    {
        s_serializer = new Serializer();
        s_cache = new Cache();
    }

    public static void RegisterProcessor<T>(params string[] extensions) where T : Processor
    {
        s_serializer._processors.Register<T>(extensions);
    }

    public static Resource Load<T>(string path)
    {
        return default;
    }

    public static async Task<Resource> LoadAsync<T>(string path)
    {
        return default;
    }

    public static bool Save<T>(T resource) where T : Resource
    {
        return default;
    }

    public static async Task<bool> SaveAsync<T>(T resource) where T : Resource
    {
        return default;
    }

    public override void Dispose()
    {
        s_cache = null;
        s_serializer = null;
    }

    internal override void Initialize()
    {
        RegisterProcessor<SpriteProcessor>("png");
        
        s_serializer.SerializeAll();
    }

    internal override void PreUpdate()
    {

    }

    internal override void PostUpdate()
    {

    }
}

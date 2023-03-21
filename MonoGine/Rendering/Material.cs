namespace MonoGine.Rendering;

public class Material
{
    private Shader[] _shaders;

    private Material()
    {

    }

    public Material(Shader shader)
    {
        _shaders = new Shader[] { shader };
    }

    public Material(Shader[] shaders)
    {
        _shaders = shaders;
    }

    public Shader this[int index]
    {
        get
        {
            return _shaders[index];
        }
    }
}

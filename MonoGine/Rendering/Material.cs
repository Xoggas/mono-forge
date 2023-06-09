namespace MonoGine.Rendering;

public class Material
{
    private Shader[] _shaders;

    public Material(Shader shader)
    {
        _shaders = new Shader[] { shader };
    }

    public Material(Shader[] shaders)
    {
        _shaders = shaders;
    }

    private Material()
    {

    }

    public Shader this[int index] => _shaders[index];
}

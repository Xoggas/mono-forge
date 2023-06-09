using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public class Shader : Object
{
    private Effect _effect;

    public Shader(Effect effect)
    {
        _effect = effect;
    }

    public EffectTechniqueCollection Techniques => _effect.Techniques;
    public EffectParameterCollection Parameters => _effect.Parameters;

    public override void Dispose()
    {
        _effect = null;
    }
}
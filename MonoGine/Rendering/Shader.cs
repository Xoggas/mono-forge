using Microsoft.Xna.Framework.Graphics;
using MonoGine.ResourceLoading;

namespace MonoGine.Rendering;

public class Shader : Resource
{
    private Effect _effect;

    internal Shader(Metadata metadata, Effect effect) : base(metadata)
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
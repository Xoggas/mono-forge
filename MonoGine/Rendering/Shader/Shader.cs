using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Shader : IObject
{
    private readonly Effect _effect;
    private readonly PropertyCollection _properties;

    public Shader(Effect effect)
    {
        _effect = effect;
        _properties = new PropertyCollection(effect.Parameters);
    }

    public PropertyCollection Properties => _properties;
    public EffectPassCollection Passes => _effect.CurrentTechnique.Passes;

    public void ApplyProperties()
    {
        Properties.ApplyTo(_effect);
    }
    
    public static bool Equals(Shader? a, Shader? b)
    {
        return a == b || a != null && b != null && a._properties.Equals(b._properties);
    }

    public void Dispose()
    {
        _effect.Dispose();
    }
}

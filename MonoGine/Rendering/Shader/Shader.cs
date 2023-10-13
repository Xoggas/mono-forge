using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Shader : IObject, IEquatable<Shader>, IDeepCopyable<Shader>
{
    private readonly Effect _effect;
    private readonly PropertyCollection _properties;

    public Shader(Effect effect)
    {
        _effect = effect;
        _properties = new PropertyCollection();
    }

    private Shader(Effect effect, PropertyCollection properties)
    {
        _effect = effect;
        _properties = properties.DeepCopy();
    }

    public PropertyCollection Properties => _properties;
    public EffectPassCollection Passes => _effect.CurrentTechnique.Passes;

    public void ApplyProperties()
    {
        Properties.ApplyTo(_effect);
    }

    public Shader DeepCopy()
    {
        return new Shader(_effect, _properties);
    }

    public bool Equals(Shader? other)
    {
        return ReferenceEquals(this, other) || (other != null && _properties.Equals(other._properties));
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Shader);
    }

    public void Dispose()
    {
    }
}
using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Shader : IObject, IEquatable<Shader>, IDeepCopyable<Shader>
{
    private readonly Effect _effect;
    private readonly Properties _properties;

    public Shader(Effect effect)
    {
        _effect = effect;
        _properties = new Properties();
    }

    private Shader(Effect effect, Properties properties)
    {
        _effect = effect;
        _properties = properties;
    }

    public Properties Properties => _properties;
    public EffectPassCollection Passes => _effect.CurrentTechnique.Passes;

    public void ApplyProperties()
    {
        Properties.ApplyTo(_effect);
    }

    public Shader DeepCopy()
    {
        return new Shader(_effect, _properties.DeepCopy());
    }

    public bool Equals(Shader? other)
    {
        return ReferenceEquals(this, other) || (other != null && _properties.Equals(other._properties));
    }

    public void Dispose()
    {
        _effect.Dispose();
    }
}
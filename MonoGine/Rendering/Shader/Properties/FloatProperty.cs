using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class FloatProperty : IProperty, IEquatable<FloatProperty>
{
    public FloatProperty(string name, float value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public float Value { get; set; }

    public void ApplyValueToEffect(Effect effect)
    {
        effect.Parameters[Name].SetValue(Value);
    }

    public IProperty DeepCopy()
    {
        return new FloatProperty(Name, Value);
    }

    public bool Equals(FloatProperty? other)
    {
        return ReferenceEquals(this, other) || (other != null && Math.Abs(Value - other.Value) < float.MinValue);
    }

    public bool Equals(IProperty? other)
    {
        return Equals(other as FloatProperty);
    }
}
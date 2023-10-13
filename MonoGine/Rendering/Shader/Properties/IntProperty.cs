using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class IntProperty : IProperty, IEquatable<IntProperty>
{
    public IntProperty(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public int Value { get; set; }

    public void ApplyValueToEffect(Effect effect)
    {
        effect.Parameters[Name].SetValue(Value);
    }

    public IProperty DeepCopy()
    {
        return new IntProperty(Name, Value);
    }

    public bool Equals(IntProperty? other)
    {
        return ReferenceEquals(this, other) || (other != null && Value == other.Value);
    }

    public bool Equals(IProperty? other)
    {
        return Equals(other as IntProperty);
    }
}
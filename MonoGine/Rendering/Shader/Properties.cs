using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Properties : IDeepCopyable<Properties>, IEquatable<Properties>
{
    private readonly Dictionary<string, IProperty> _properties = new();

    private static readonly Dictionary<Type, Func<IProperty>> s_propertyTypeLookup = new()
    {
        { typeof(int), () => new IntProperty() },
        { typeof(int[]), () => new IntBufferProperty() },
        { typeof(float), () => new FloatProperty() },
        { typeof(float[]), () => new FloatBufferProperty() }
    };

    internal Properties()
    {
    }

    private Properties(Dictionary<string, IProperty> properties)
    {
        foreach ((var key, IProperty value) in properties)
        {
            _properties.Add(key, value.DeepCopy());
        }
    }

    public T Get<T>(string name)
    {
        if (_properties[name] is Property<T> specificProperty)
        {
            return specificProperty.Value;
        }

        throw new InvalidCastException($"Can't cast property {name} to type {typeof(T)}");
    }

    //TODO: Add property instantiation
    public void Set<T>(string name, T value)
    {
        if (_properties[name] is Property<T> specificProperty)
        {
            specificProperty.Value = value;
        }
        else
        {
            throw new InvalidCastException();
        }
    }

    public void ApplyTo(Effect effect)
    {
        foreach ((var name, IProperty property) in _properties)
        {
            property.ApplyProperty(effect, name);
        }
    }

    public Properties DeepCopy()
    {
        return new Properties(_properties);
    }

    public bool Equals(Properties? other)
    {
        if (other == null)
        {
            return false;
        }

        if (_properties.Count != other._properties.Count)
        {
            return false;
        }

        return _properties.Keys.All(key =>
            other._properties.ContainsKey(key) && _properties[key].Equals(other._properties[key]));
    }

    public override bool Equals(object? other)
    {
        return ReferenceEquals(this, other) || (other is Properties properties && Equals(properties));
    }

    public override int GetHashCode()
    {
        return _properties.GetHashCode();
    }

    private void AddNewPropertyForType<T>(string name, T value)
    {
    }
}
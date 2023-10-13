using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class PropertyCollection : IObject, IEnumerable<IProperty>, IDeepCopyable<PropertyCollection>,
    IEquatable<PropertyCollection>
{
    private readonly Dictionary<string, IProperty> _properties = new();

    internal PropertyCollection()
    {
    }

    private PropertyCollection(Dictionary<string, IProperty> properties)
    {
        foreach ((var key, IProperty value) in properties)
        {
            _properties.Add(key, value.DeepCopy());
        }
    }

    public void ApplyTo(Effect effect)
    {
        foreach (IProperty property in _properties.Values)
        {
            property.ApplyValueToEffect(effect);
        }
    }

    public void Add<T>(T property) where T : class, IProperty
    {
        _properties.Add(property.Name, property);
    }

    public T? Get<T>(string name) where T : class, IProperty
    {
        if (_properties.TryGetValue(name, out IProperty? property))
        {
            return property as T;
        }

        return default;
    }

    public PropertyCollection DeepCopy()
    {
        return new PropertyCollection(_properties);
    }

    public bool Equals(PropertyCollection? other)
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

    public IEnumerator<IProperty> GetEnumerator()
    {
        return _properties.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is PropertyCollection other && Equals(other));
    }

    public override int GetHashCode()
    {
        return _properties.GetHashCode();
    }

    public void Dispose()
    {
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public abstract class Property<T> : IProperty, IDeepCopyable<Property<T>>
{
    public T Value { get; set; } = default!;

    public abstract void ApplyProperty(Effect effect, string propertyName);
    public abstract Property<T> DeepCopy();

    public bool Equals(IProperty? other)
    {
        if (other is not Property<T> otherProperty)
        {
            return false;
        }

        if (Value is IEnumerable<T> leftCollection && otherProperty.Value is IEnumerable<T> rightCollection)
        {
            return leftCollection.SequenceEqual(rightCollection);
        }

        return Value != null && Value.Equals(otherProperty.Value);
    }

    IProperty IDeepCopyable<IProperty>.DeepCopy()
    {
        return DeepCopy();
    }
}
using System;

namespace MonoForge.Animations;

/// <summary>
/// An interfaces for handling animatable values;
/// </summary>
public interface IAnimatable
{
    /// <summary>
    /// Returns the child by name.
    /// </summary>
    /// <param name="name">Name of the child.</param>
    /// <returns>The first child with specified name.</returns>
    public IAnimatable? GetChild(ReadOnlySpan<char> name);

    /// <summary>
    /// Returns the cached property setter by property name.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <returns>The cached property.</returns>
    public Action<float> GetPropertySetter(ReadOnlySpan<char> name);
}
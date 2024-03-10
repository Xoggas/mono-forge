using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public interface IProperty : IEquatable<IProperty>, IDeepCopyable<IProperty>
{
    public void ApplyProperty(Effect effect, string propertyName);
}
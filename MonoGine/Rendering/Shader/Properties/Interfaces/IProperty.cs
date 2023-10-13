using System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public interface IProperty : IEquatable<IProperty>, IDeepCopyable<IProperty>
{
    public string Name { get; }
    public void ApplyValueToEffect(Effect effect);
}
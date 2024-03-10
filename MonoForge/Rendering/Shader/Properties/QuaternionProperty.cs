using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class QuaternionProperty : Property<Quaternion>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Quaternion> DeepCopy()
    {
        return new QuaternionProperty { Value = Value };
    }
}
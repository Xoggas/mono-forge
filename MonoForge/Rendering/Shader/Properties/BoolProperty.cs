using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class BoolProperty : Property<bool>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<bool> DeepCopy()
    {
        return new BoolProperty { Value = Value };
    }
}
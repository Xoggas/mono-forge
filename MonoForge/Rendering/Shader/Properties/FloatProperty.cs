using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class FloatProperty : Property<float>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<float> DeepCopy()
    {
        return new FloatProperty { Value = Value };
    }
}

public sealed class FloatBufferProperty : Property<float[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<float[]> DeepCopy()
    {
        return new FloatBufferProperty { Value = (float[])Value.Clone() };
    }
}
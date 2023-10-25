using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class IntProperty : Property<int>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<int> DeepCopy()
    {
        return new IntProperty { Value = Value };
    }
}

public sealed class IntBufferProperty : Property<int[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<int[]> DeepCopy()
    {
        return new IntBufferProperty { Value = (int[])Value.Clone() };
    }
}
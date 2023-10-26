using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Vector4Property : Property<Vector4>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector4> DeepCopy()
    {
        return new Vector4Property { Value = Value };
    }
}

public sealed class Vector4BufferProperty : Property<Vector4[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector4[]> DeepCopy()
    {
        return new Vector4BufferProperty { Value = (Vector4[])Value.Clone() };
    }
}
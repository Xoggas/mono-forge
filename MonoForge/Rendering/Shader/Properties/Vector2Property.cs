using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class Vector2Property : Property<Vector2>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector2> DeepCopy()
    {
        return new Vector2Property { Value = Value };
    }
}

public sealed class Vector2BufferProperty : Property<Vector2[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector2[]> DeepCopy()
    {
        return new Vector2BufferProperty { Value = (Vector2[])Value.Clone() };
    }
}
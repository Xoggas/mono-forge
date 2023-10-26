using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class Vector3Property : Property<Vector3>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector3> DeepCopy()
    {
        return new Vector3Property { Value = Value };
    }
}

public sealed class Vector3BufferProperty : Property<Vector3[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Vector3[]> DeepCopy()
    {
        return new Vector3BufferProperty { Value = (Vector3[])Value.Clone() };
    }
}
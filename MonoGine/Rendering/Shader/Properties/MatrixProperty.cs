using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class MatrixProperty : Property<Matrix>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Matrix> DeepCopy()
    {
        return new MatrixProperty { Value = Value };
    }
}

public sealed class MatrixBufferProperty : Property<Matrix[]>
{
    public override void ApplyProperty(Effect effect, string propertyName)
    {
        effect.Parameters[propertyName].SetValue(Value);
    }

    public override Property<Matrix[]> DeepCopy()
    {
        return new MatrixBufferProperty { Value = (Matrix[])Value.Clone() };
    }
}
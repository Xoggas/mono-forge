using Microsoft.Xna.Framework.Graphics;
using MonoForge.Rendering;

namespace MonoForge.Extensions;

public static class EffectExtensions
{
    public static Shader ToShader(this Effect effect)
    {
        return Shader.FromEffect(effect);
    }
}
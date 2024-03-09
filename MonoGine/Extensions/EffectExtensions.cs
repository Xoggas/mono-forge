using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.Extensions;

public static class EffectExtensions
{
    public static Shader ToShader(this Effect effect)
    {
        return Shader.FromEffect(effect);
    }
}
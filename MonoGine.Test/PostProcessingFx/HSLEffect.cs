using Microsoft.Xna.Framework.Graphics;
using MonoGine.Graphics.PostProcessing;

namespace MonoGine.Test.PostProcessing;

public sealed class HSLEffect : IPostProcessingEffect
{
    private Effect _effect;

    public HSLEffect(Effect effect)
    {
        _effect = effect.Clone();
    }

    public bool Enabled { get; set; }

    public float Hue
    {
        get => _effect.Parameters["Hue"].GetValueSingle();
        set => _effect.Parameters["Hue"].SetValue(value);
    }

    public float Saturation
    {
        get => _effect.Parameters["Saturation"].GetValueSingle();
        set => _effect.Parameters["Saturation"].SetValue(value);
    }

    public float Lightness
    {
        get => _effect.Parameters["Lightness"].GetValueSingle();
        set => _effect.Parameters["Lightness"].SetValue(value);
    }

    public void Apply()
    {
        _effect.CurrentTechnique.Passes[0].Apply();
    }

    public void Dispose()
    {
        _effect.Dispose();
    }
}

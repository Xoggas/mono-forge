using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public class Shader : Object
{
    private Effect _effect;

    public void ApplyPass(int index)
    {
        
    }

    public void SetValue(string name, bool value)
    {
        _effect.Parameters[name].SetValue(value);
    }

    public void SetValue(string name, int value)
    {
        _effect.Parameters[name].SetValue(value);
    }

    public void SetValues(string name, int[] values)
    {
        _effect.Parameters[name].SetValue(values);
    }

    public void SetValue(string name, float value)
    {
        _effect.Parameters[name].SetValue(value);
    }

    public void SetValues(string name, float[] values)
    {
        _effect.Parameters[name].SetValue(values);
    }

    public override void Dispose()
    {
        _effect = null;
    }
}
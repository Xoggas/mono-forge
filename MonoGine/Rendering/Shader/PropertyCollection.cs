using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering;

public sealed class PropertyCollection : IObject, IEquatable<PropertyCollection>
{
    private readonly Dictionary<string, object?> _properties;

    internal PropertyCollection(EffectParameterCollection parameters)
    {
        _properties = parameters.ToDictionary<EffectParameter, string, object?>(x => x.Name, x =>
        {
            if (x.ParameterType == EffectParameterType.Int32)
            {
                return x.GetValueInt32();
            }

            if (x.ParameterClass == EffectParameterClass.Matrix)
            {
                return x.GetValueMatrix();
            }

            if (x.ParameterType is EffectParameterType.Texture or EffectParameterType.Texture2D)
            {
                return x.GetValueTexture2D();
            }

            if (x.ParameterType == EffectParameterType.Single)
            {
                return x.Elements.Count switch
                {
                    4 => x.GetValueVector4(),
                    3 => x.GetValueVector3(),
                    2 => x.GetValueVector2(),
                    1 => x.GetValueSingle(),
                    _ => 0f
                };
            }

            return null;
        });
    }

    public void SetInt(string key, int value)
    {
        SetValue(key, value);
    }

    public void SetFloat(string key, float value)
    {
        SetValue(key, value);
    }

    public void SetVector2(string key, Vector2 value)
    {
        SetValue(key, value);
    }

    public void SetVector3(string key, Vector3 value)
    {
        SetValue(key, value);
    }

    public void SetColor(string key, Color value)
    {
        SetValue(key, value);
    }

    public void SetMatrix(string key, Matrix value)
    {
        SetValue(key, value);
    }

    public void SetTexture(string key, Texture2D value)
    {
        SetValue(key, value);
    }

    public int GetInt(string key)
    {
        return GetValue<int>(key);
    }

    public float GetFloat(string key)
    {
        return GetValue<float>(key);
    }

    public Vector2 GetVector2(string key)
    {
        return GetValue<Vector2>(key);
    }

    public Vector3 GetVector3(string key)
    {
        return GetValue<Vector3>(key);
    }

    public Color GetColor(string key)
    {
        return GetValue<Color>(key);
    }

    public Matrix GetMatrix(string key)
    {
        return GetValue<Matrix>(key);
    }

    public Texture2D GetTexture(string key)
    {
        return GetValue<Texture2D>(key);
    }

    public void ApplyTo(Effect effect)
    {
        foreach (var (key, value) in _properties)
        {
            switch (value)
            {
                case int intValue:
                    effect.Parameters[key].SetValue(intValue);
                    break;
                case float floatValue:
                    effect.Parameters[key].SetValue(floatValue);
                    break;
                case Vector2 vector2Value:
                    effect.Parameters[key].SetValue(vector2Value);
                    break;
                case Vector3 vector3Value:
                    effect.Parameters[key].SetValue(vector3Value);
                    break;
                case Color colorValue:
                    effect.Parameters[key].SetValue(colorValue.ToVector4());
                    break;
                case Matrix matrixValue:
                    effect.Parameters[key].SetValue(matrixValue);
                    break;
                case Texture2D textureValue:
                    effect.Parameters[key].SetValue(textureValue);
                    break;
            }
        }
    }

    public void Dispose()
    {
        _properties.Clear();
    }

    private void SetValue(string key, object value)
    {
        if (_properties.ContainsKey(key))
        {
            _properties[key] = value;
        }
        else
        {
            throw new ArgumentException($"Property {key} doesn't exist!");
        }
    }

    private T GetValue<T>(string key)
    {
        if (_properties.TryGetValue(key, out var value) && value is T castedValue)
        {
            return castedValue;
        }
        else
        {
            throw new InvalidCastException();
        }
    }

    public bool Equals(PropertyCollection? other)
    {
        if (other == null)
        {
            return true;
        }

        if (_properties.Count != other?._properties.Count)
        {
            return false;
        }

        foreach (var (key, value) in _properties)
        {
            if (value == null || !other._properties.ContainsKey(key) && !value.Equals(other._properties[key]))
            {
                return false;
            }
        }

        return true;
    }
}

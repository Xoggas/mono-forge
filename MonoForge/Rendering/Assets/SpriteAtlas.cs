using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

//TODO: Think about extensibility of loading
public sealed class SpriteAtlas : IDisposable
{
    private readonly Texture2D _texture;
    private readonly Dictionary<string, Sprite> _spriteNameDictionary;
    private readonly Dictionary<int, Sprite> _spriteIdDictionary;

    //TODO: Fix naming
    public SpriteAtlas(Texture2D texture, Dictionary<string, Sprite> spriteNames, Dictionary<int, Sprite> spriteIds)
    {
        _texture = texture;
        _spriteNameDictionary = spriteNames;
        _spriteIdDictionary = spriteIds;
    }

    public Sprite GetSpriteByName(string name)
    {
        return _spriteNameDictionary[name];
    }

    public Sprite GetSpriteById(int id)
    {
        return _spriteIdDictionary[id];
    }

    public void Dispose()
    {
        _texture.Dispose();
    }
}
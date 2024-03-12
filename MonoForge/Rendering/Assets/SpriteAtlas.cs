using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace MonoForge.Rendering;

public sealed class SpriteAtlas : IDisposable
{
    private readonly Texture2D _atlas;
    private readonly Dictionary<string, Sprite> _spriteNameDictionary;
    private readonly Dictionary<int, Sprite> _spriteIdDictionary;

    public SpriteAtlas(Texture2D atlas, IReadOnlyCollection<Sprite> sprites)
    {
        _atlas = atlas;
        _spriteNameDictionary = sprites.ToDictionary(x => x.Name);
        _spriteIdDictionary = sprites.ToDictionary(x => x.Id);
    }

    public Sprite this[int id] => _spriteIdDictionary[id];
    public Sprite this[string name] => _spriteNameDictionary[name];

    public void Dispose()
    {
        _atlas.Dispose();
    }
}
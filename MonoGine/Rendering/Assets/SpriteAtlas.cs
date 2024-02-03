using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.AssetLoading;

namespace MonoGine.Rendering;

//TODO: Think about extensibility of loading
public sealed class SpriteAtlas : IAsset
{
    private readonly Texture2D _texture;
    private readonly Dictionary<string, Sprite> _spriteNameDictionary;
    private readonly Dictionary<int, Sprite> _spriteIdDictionary;

    internal SpriteAtlas(Texture2D texture, IList<SpriteInfo> data)
    {
        _texture = texture;
        _spriteNameDictionary = data.ToDictionary(x => x.Name, x => new Sprite(texture, x.Bounds));
        _spriteIdDictionary = data.ToDictionary(x => x.Id, x => new Sprite(texture, x.Bounds));
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
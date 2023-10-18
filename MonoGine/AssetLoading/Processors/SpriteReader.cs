using Microsoft.Xna.Framework.Graphics;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

internal sealed class SpriteReader : IAssetReader<Sprite>
{
    public Sprite Read(IEngine engine, string path)
    {
        return new Sprite(Texture2D.FromFile(engine.GraphicsDevice, PathUtils.GetAbsolutePath(path)));
    }
}
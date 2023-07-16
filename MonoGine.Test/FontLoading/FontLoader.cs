using Microsoft.Xna.Framework;
using MonoGine.Resources;
using SharpFont;

namespace MonoGine.Test.FontLoading;

public sealed class FontLoader : IObject
{
    private readonly Library _library;

    public FontLoader()
    {
        _library = new Library();
    }

    public Face LoadFont()
    {
        return new Face(_library, PathUtils.GetAbsolutePath("Fonts/Nexa Light.otf"));
    }

    public Glyph GetGlyph(Face face, char symbol)
    {
        face.SetPixelSizes(0, 1024);
        face.LoadChar(symbol, LoadFlags.Render, LoadTarget.Normal);

        var glyph = face.Glyph;
        var bitmap = glyph.Bitmap;

        return new Glyph(bitmap.BufferData, new Point(bitmap.Width, bitmap.Rows), new Point(glyph.BitmapLeft, glyph.BitmapTop), glyph.Advance.X);
    }

    public void Dispose()
    {
    }
}

public readonly struct Glyph
{
    public readonly byte[] Bitmap;
    public readonly Point Size;
    public readonly Point Bearing;
    public readonly int Advance;

    public Glyph(byte[] bitmap, Point size, Point bearing, int advance)
    {
        Bitmap = bitmap;
        Size = size;
        Bearing = bearing;
        Advance = advance;
    }
}

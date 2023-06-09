using System;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace MGFXC.Effect.TPGParser;

public static class ParseTreeTools
{
	public static float ParseFloat(string value)
	{
		value = value.Replace(" ", "");
		value = value.TrimEnd('f', 'F');
		return float.Parse(value, NumberStyles.Float, CultureInfo.InvariantCulture);
	}

	public static int ParseInt(string value)
	{
		return (int)Math.Floor(ParseFloat(value));
	}

	public static bool ParseBool(string value)
	{
		if (value.ToLowerInvariant() == "true" || value == "1")
		{
			return true;
		}
		if (value.ToLowerInvariant() == "false" || value == "0")
		{
			return false;
		}
		throw new Exception("Invalid boolean value '" + value + "'");
	}

	public static Color ParseColor(string value)
	{
		uint hexValue = Convert.ToUInt32(value, 16);
		byte r;
		byte g;
		byte b;
		byte a;
		if (value.Length == 8)
		{
			r = (byte)((hexValue >> 16) & 0xFFu);
			g = (byte)((hexValue >> 8) & 0xFFu);
			b = (byte)(hexValue & 0xFFu);
			a = byte.MaxValue;
		}
		else
		{
			if (value.Length != 10)
			{
				throw new NotSupportedException();
			}
			r = (byte)((hexValue >> 24) & 0xFFu);
			g = (byte)((hexValue >> 16) & 0xFFu);
			b = (byte)((hexValue >> 8) & 0xFFu);
			a = (byte)(hexValue & 0xFFu);
		}
		return new Color(r, g, b, a);
	}
}

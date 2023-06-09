using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Content.Pipeline;
using MGFXC.Effect.TPGParser;

namespace MGFXC.Effect;

[TypeConverter(typeof(StringConverter))]
public abstract class ShaderProfile
{
	private class StringConverter : TypeConverter
	{
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string name = value as string;
				foreach (ShaderProfile e in All)
				{
					if (e.Name == name)
					{
						return e;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}

	private static readonly LoadedTypeCollection<ShaderProfile> _profiles = new LoadedTypeCollection<ShaderProfile>();

	public static readonly ShaderProfile OpenGL = FromName("OpenGL");

	public static readonly ShaderProfile DirectX_11 = FromName("DirectX_11");

	public static IEnumerable<ShaderProfile> All => _profiles;

	public string Name { get; private set; }

	public byte FormatId { get; private set; }

	protected ShaderProfile(string name, byte formatId)
	{
		Name = name;
		FormatId = formatId;
	}

	public static ShaderProfile FromName(string name)
	{
		return _profiles.FirstOrDefault((ShaderProfile p) => p.Name == name);
	}

	internal abstract void AddMacros(Dictionary<string, string> macros);

	internal abstract void ValidateShaderModels(PassInfo pass);

	internal abstract ShaderData CreateShader(ShaderResult shaderResult, string shaderFunction, string shaderProfile, bool isVertexShader, EffectObject effect, ref string errorsAndWarnings);

	protected static void ParseShaderModel(string text, Regex regex, out int major, out int minor)
	{
		Match match = regex.Match(text);
		if (!match.Success)
		{
			major = 0;
			minor = 0;
		}
		else
		{
			major = int.Parse(match.Groups["major"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
			minor = int.Parse(match.Groups["minor"].Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
		}
	}
}

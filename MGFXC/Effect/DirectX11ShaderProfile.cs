using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MGFXC.Effect.TPGParser;

namespace MGFXC.Effect;

internal class DirectX11ShaderProfile : ShaderProfile
{
	private static readonly Regex HlslPixelShaderRegex = new Regex("^ps_(?<major>1|2|3|4|5)_(?<minor>0|1|)(_level_(9_1|9_2|9_3))?$", RegexOptions.Compiled);

	private static readonly Regex HlslVertexShaderRegex = new Regex("^vs_(?<major>1|2|3|4|5)_(?<minor>0|1|)(_level_(9_1|9_2|9_3))?$", RegexOptions.Compiled);

	public DirectX11ShaderProfile()
		: base("DirectX_11", 1)
	{
	}

	internal override void AddMacros(Dictionary<string, string> macros)
	{
		macros.Add("HLSL", "1");
		macros.Add("SM4", "1");
	}

	internal override void ValidateShaderModels(PassInfo pass)
	{
		int major;
		int minor;
		if (!string.IsNullOrEmpty(pass.vsFunction))
		{
			ShaderProfile.ParseShaderModel(pass.vsModel, HlslVertexShaderRegex, out major, out minor);
			if (major <= 3)
			{
				throw new Exception($"Invalid profile '{pass.vsModel}'. Vertex shader '{pass.vsFunction}' must be SM 4.0 level 9.1 or higher!");
			}
		}
		if (!string.IsNullOrEmpty(pass.psFunction))
		{
			ShaderProfile.ParseShaderModel(pass.psModel, HlslPixelShaderRegex, out major, out minor);
			if (major <= 3)
			{
				throw new Exception($"Invalid profile '{pass.vsModel}'. Pixel shader '{pass.psFunction}' must be SM 4.0 level 9.1 or higher!");
			}
		}
	}

	internal override ShaderData CreateShader(ShaderResult shaderResult, string shaderFunction, string shaderProfile, bool isVertexShader, EffectObject effect, ref string errorsAndWarnings)
	{
		byte[] bytecode = EffectObject.CompileHLSL(shaderResult, shaderFunction, shaderProfile, ref errorsAndWarnings);
		foreach (ShaderData shader in effect.Shaders)
		{
			if (bytecode.SequenceEqual(shader.Bytecode))
			{
				return shader;
			}
		}
		ShaderInfo shaderInfo = shaderResult.ShaderInfo;
		ShaderData shaderData = ShaderData.CreateHLSL(bytecode, isVertexShader, effect.ConstantBuffers, effect.Shaders.Count, shaderInfo.SamplerStates, shaderResult.Debug);
		effect.Shaders.Add(shaderData);
		return shaderData;
	}
}

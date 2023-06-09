using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MGFXC.Effect.TPGParser;

namespace MGFXC.Effect;

internal class OpenGLShaderProfile : ShaderProfile
{
	private static readonly Regex GlslPixelShaderRegex = new Regex("^ps_(?<major>1|2|3|4|5)_(?<minor>0|1|)$", RegexOptions.Compiled);

	private static readonly Regex GlslVertexShaderRegex = new Regex("^vs_(?<major>1|2|3|4|5)_(?<minor>0|1|)$", RegexOptions.Compiled);

	public OpenGLShaderProfile()
		: base("OpenGL", 0)
	{
	}

	internal override void AddMacros(Dictionary<string, string> macros)
	{
		macros.Add("GLSL", "1");
		macros.Add("OPENGL", "1");
	}

	internal override void ValidateShaderModels(PassInfo pass)
	{
		int major;
		int minor;
		if (!string.IsNullOrEmpty(pass.vsFunction))
		{
			ShaderProfile.ParseShaderModel(pass.vsModel, GlslVertexShaderRegex, out major, out minor);
			if (major > 3)
			{
				throw new Exception($"Invalid profile '{pass.vsModel}'. Vertex shader '{pass.vsFunction}' must be SM 3.0 or lower!");
			}
		}
		if (!string.IsNullOrEmpty(pass.psFunction))
		{
			ShaderProfile.ParseShaderModel(pass.psModel, GlslPixelShaderRegex, out major, out minor);
			if (major > 3)
			{
				throw new Exception($"Invalid profile '{pass.vsModel}'. Pixel shader '{pass.psFunction}' must be SM 3.0 or lower!");
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
		ShaderData shaderData = ShaderData.CreateGLSL(bytecode, isVertexShader, effect.ConstantBuffers, effect.Shaders.Count, shaderInfo.SamplerStates, shaderResult.Debug);
		effect.Shaders.Add(shaderData);
		return shaderData;
	}
}

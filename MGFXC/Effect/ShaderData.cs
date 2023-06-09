using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MGFXC.Effect.TPGParser;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;

namespace MGFXC.Effect;

internal class ShaderData
{
	public struct Sampler
	{
		public MojoShader.MOJOSHADER_samplerType type;

		public int textureSlot;

		public int samplerSlot;

		public string samplerName;

		public string parameterName;

		public int parameter;

		public SamplerState state;
	}

	public struct Attribute
	{
		public string name;

		public VertexElementUsage usage;

		public int index;

		public int location;
	}

	public int[] _cbuffers;

	public Sampler[] _samplers;

	public Attribute[] _attributes;

	public bool IsVertexShader { get; private set; }

	public byte[] ShaderCode { get; set; }

	public byte[] Bytecode { get; private set; }

	public int SharedIndex { get; private set; }

	public ShaderData(bool isVertexShader, int sharedIndex, byte[] bytecode)
	{
		IsVertexShader = isVertexShader;
		SharedIndex = sharedIndex;
		Bytecode = (byte[])bytecode.Clone();
	}

	public static ShaderData CreateGLSL(byte[] byteCode, bool isVertexShader, List<ConstantBufferData> cbuffers, int sharedIndex, Dictionary<string, SamplerStateInfo> samplerStates, bool debug)
	{
		ShaderData dxshader = new ShaderData(isVertexShader, sharedIndex, byteCode);
		MojoShader.MOJOSHADER_parseData parseData = MarshalHelper.Unmarshal<MojoShader.MOJOSHADER_parseData>(MojoShader.NativeMethods.MOJOSHADER_parse("glsl", byteCode, byteCode.Length, IntPtr.Zero, 0, IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
		if (parseData.error_count > 0)
		{
			throw new Exception(MarshalHelper.UnmarshalArray<MojoShader.MOJOSHADER_error>(parseData.errors, parseData.error_count)[0].error);
		}
		MojoShader.MOJOSHADER_attribute[] attributes = MarshalHelper.UnmarshalArray<MojoShader.MOJOSHADER_attribute>(parseData.attributes, parseData.attribute_count);
		dxshader._attributes = new Attribute[attributes.Length];
		for (int l = 0; l < attributes.Length; l++)
		{
			dxshader._attributes[l].name = attributes[l].name;
			dxshader._attributes[l].index = attributes[l].index;
			dxshader._attributes[l].usage = EffectObject.ToXNAVertexElementUsage(attributes[l].usage);
		}
		MojoShader.MOJOSHADER_symbol[] symbols = MarshalHelper.UnmarshalArray<MojoShader.MOJOSHADER_symbol>(parseData.symbols, parseData.symbol_count);
		Array.Sort(symbols, delegate(MojoShader.MOJOSHADER_symbol a, MojoShader.MOJOSHADER_symbol b)
		{
			uint num = a.register_index;
			if (a.info.elements == 1)
			{
				num += 1024;
			}
			uint num2 = b.register_index;
			if (b.info.elements == 1)
			{
				num2 += 1024;
			}
			return num.CompareTo(num2);
		});
		uint bool_index = 0u;
		uint float4_index = 0u;
		uint int4_index = 0u;
		for (int k = 0; k < symbols.Length; k++)
		{
			switch (symbols[k].register_set)
			{
			case MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_BOOL:
				symbols[k].register_index = bool_index;
				bool_index += symbols[k].register_count;
				break;
			case MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_FLOAT4:
				symbols[k].register_index = float4_index;
				float4_index += symbols[k].register_count;
				break;
			case MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_INT4:
				symbols[k].register_index = int4_index;
				int4_index += symbols[k].register_count;
				break;
			}
		}
		MojoShader.MOJOSHADER_sampler[] samplers = MarshalHelper.UnmarshalArray<MojoShader.MOJOSHADER_sampler>(parseData.samplers, parseData.sampler_count);
		dxshader._samplers = new Sampler[samplers.Length];
		int i;
		for (i = 0; i < samplers.Length; i++)
		{
			string originalSamplerName = symbols.First((MojoShader.MOJOSHADER_symbol e) => e.register_set == MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_SAMPLER && e.register_index == samplers[i].index).name;
			Sampler sampler2 = default(Sampler);
			sampler2.parameter = -1;
			sampler2.samplerName = samplers[i].name;
			sampler2.parameterName = originalSamplerName;
			sampler2.textureSlot = samplers[i].index;
			sampler2.samplerSlot = samplers[i].index;
			sampler2.type = samplers[i].type;
			Sampler sampler = sampler2;
			if (samplerStates.TryGetValue(originalSamplerName, out var state))
			{
				sampler.state = state.State;
				sampler.parameterName = state.TextureName ?? originalSamplerName;
			}
			dxshader._samplers[i] = sampler;
		}
		var symbol_types = new[]
		{
			new
			{
				name = (dxshader.IsVertexShader ? "vs_uniforms_bool" : "ps_uniforms_bool"),
				set = MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_BOOL
			},
			new
			{
				name = (dxshader.IsVertexShader ? "vs_uniforms_ivec4" : "ps_uniforms_ivec4"),
				set = MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_INT4
			},
			new
			{
				name = (dxshader.IsVertexShader ? "vs_uniforms_vec4" : "ps_uniforms_vec4"),
				set = MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_FLOAT4
			}
		};
		List<int> cbuffer_index = new List<int>();
		for (int j = 0; j < symbol_types.Length; j++)
		{
			ConstantBufferData cbuffer = new ConstantBufferData(symbol_types[j].name, symbol_types[j].set, symbols);
			if (cbuffer.Size != 0)
			{
				int match = cbuffers.FindIndex((ConstantBufferData e) => e.SameAs(cbuffer));
				if (match == -1)
				{
					cbuffer_index.Add(cbuffers.Count);
					cbuffers.Add(cbuffer);
				}
				else
				{
					cbuffer_index.Add(match);
				}
			}
		}
		dxshader._cbuffers = cbuffer_index.ToArray();
		string glslCode = parseData.output;
		glslCode = glslCode.Replace("#version 110", "");
		string floatPrecision = (dxshader.IsVertexShader ? "precision highp float;\r\n" : "precision mediump float;\r\n");
		glslCode = "#ifdef GL_ES\r\n" + floatPrecision + "precision mediump int;\r\n#endif\r\n" + glslCode;
		if (glslCode.IndexOf("dFdx", StringComparison.InvariantCulture) >= 0 || glslCode.IndexOf("dFdy", StringComparison.InvariantCulture) >= 0)
		{
			glslCode = "#extension GL_OES_standard_derivatives : enable\r\n" + glslCode;
		}
		dxshader.ShaderCode = Encoding.ASCII.GetBytes(glslCode);
		return dxshader;
	}

	public static ShaderData CreatePSSL(byte[] byteCode, bool isVertexShader, List<ConstantBufferData> cbuffers, int sharedIndex, Dictionary<string, SamplerStateInfo> samplerStates, bool debug)
	{
		throw new NotImplementedException();
	}

	public static ShaderData CreateHLSL(byte[] byteCode, bool isVertexShader, List<ConstantBufferData> cbuffers, int sharedIndex, Dictionary<string, SamplerStateInfo> samplerStates, bool debug)
	{
		ShaderData dxshader = new ShaderData(isVertexShader, sharedIndex, byteCode);
		dxshader._attributes = new Attribute[0];
		StripFlags stripFlags = StripFlags.CompilerStripReflectionData | StripFlags.CompilerStripTestBlobs;
		if (!debug)
		{
			stripFlags |= StripFlags.CompilerStripDebugInformation;
		}
		using ShaderBytecode original = new ShaderBytecode(byteCode);
		ShaderBytecode stripped = original.Strip(stripFlags);
		if (stripped != null)
		{
			dxshader.ShaderCode = stripped;
		}
		else
		{
			dxshader.ShaderCode = (byte[])dxshader.Bytecode.Clone();
		}
		using ShaderReflection refelect = new ShaderReflection(byteCode);
		List<Sampler> samplers = new List<Sampler>();
		for (int j = 0; j < refelect.Description.BoundResources; j++)
		{
			InputBindingDescription rdesc = refelect.GetResourceBindingDescription(j);
			if (rdesc.Type != ShaderInputType.Texture)
			{
				continue;
			}
			string samplerName = rdesc.Name;
			Sampler sampler2 = default(Sampler);
			sampler2.samplerName = string.Empty;
			sampler2.textureSlot = rdesc.BindPoint;
			sampler2.samplerSlot = rdesc.BindPoint;
			sampler2.parameterName = samplerName;
			Sampler sampler = sampler2;
			if (samplerStates.TryGetValue(samplerName, out var state))
			{
				sampler.parameterName = state.TextureName ?? samplerName;
				sampler.state = state.State;
			}
			else
			{
				foreach (SamplerStateInfo s in samplerStates.Values)
				{
					if (samplerName == s.TextureName)
					{
						sampler.state = s.State;
						samplerName = s.Name;
						break;
					}
				}
			}
			for (int k = 0; k < refelect.Description.BoundResources; k++)
			{
				InputBindingDescription samplerrdesc = refelect.GetResourceBindingDescription(k);
				if (samplerrdesc.Type == ShaderInputType.Sampler && samplerrdesc.Name == samplerName)
				{
					sampler.samplerSlot = samplerrdesc.BindPoint;
					break;
				}
			}
			switch (rdesc.Dimension)
			{
			case ShaderResourceViewDimension.Texture1D:
			case ShaderResourceViewDimension.Texture1DArray:
				sampler.type = MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_1D;
				break;
			case ShaderResourceViewDimension.Texture2D:
			case ShaderResourceViewDimension.Texture2DArray:
			case ShaderResourceViewDimension.Texture2DMultisampled:
			case ShaderResourceViewDimension.Texture2DMultisampledArray:
				sampler.type = MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_2D;
				break;
			case ShaderResourceViewDimension.Texture3D:
				sampler.type = MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_VOLUME;
				break;
			case ShaderResourceViewDimension.TextureCube:
			case ShaderResourceViewDimension.TextureCubeArray:
				sampler.type = MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_CUBE;
				break;
			}
			samplers.Add(sampler);
		}
		dxshader._samplers = samplers.ToArray();
		dxshader._cbuffers = new int[refelect.Description.ConstantBuffers];
		for (int i = 0; i < refelect.Description.ConstantBuffers; i++)
		{
			ConstantBufferData cb = new ConstantBufferData(refelect.GetConstantBuffer(i));
			for (int c = 0; c < cbuffers.Count; c++)
			{
				if (cb.SameAs(cbuffers[c]))
				{
					cb = null;
					dxshader._cbuffers[i] = c;
					break;
				}
			}
			if (cb != null)
			{
				dxshader._cbuffers[i] = cbuffers.Count;
				cbuffers.Add(cb);
			}
		}
		return dxshader;
	}

	public void Write(BinaryWriter writer, Options options)
	{
		writer.Write(IsVertexShader);
		writer.Write(ShaderCode.Length);
		writer.Write(ShaderCode);
		writer.Write((byte)_samplers.Length);
		Sampler[] samplers = _samplers;
		for (int i = 0; i < samplers.Length; i++)
		{
			Sampler sampler = samplers[i];
			writer.Write((byte)sampler.type);
			writer.Write((byte)sampler.textureSlot);
			writer.Write((byte)sampler.samplerSlot);
			if (sampler.state != null)
			{
				writer.Write(value: true);
				writer.Write((byte)sampler.state.AddressU);
				writer.Write((byte)sampler.state.AddressV);
				writer.Write((byte)sampler.state.AddressW);
				writer.Write(sampler.state.BorderColor.R);
				writer.Write(sampler.state.BorderColor.G);
				writer.Write(sampler.state.BorderColor.B);
				writer.Write(sampler.state.BorderColor.A);
				writer.Write((byte)sampler.state.Filter);
				writer.Write(sampler.state.MaxAnisotropy);
				writer.Write(sampler.state.MaxMipLevel);
				writer.Write(sampler.state.MipMapLevelOfDetailBias);
			}
			else
			{
				writer.Write(value: false);
			}
			writer.Write(sampler.samplerName);
			writer.Write((byte)sampler.parameter);
		}
		writer.Write((byte)_cbuffers.Length);
		int[] cbuffers = _cbuffers;
		foreach (int cb in cbuffers)
		{
			writer.Write((byte)cb);
		}
		writer.Write((byte)_attributes.Length);
		Attribute[] attributes = _attributes;
		for (int i = 0; i < attributes.Length; i++)
		{
			Attribute attrib = attributes[i];
			writer.Write(attrib.name);
			writer.Write((byte)attrib.usage);
			writer.Write((byte)attrib.index);
			writer.Write((short)attrib.location);
		}
	}
}

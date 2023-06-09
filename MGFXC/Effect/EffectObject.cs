using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using MGFXC.Effect.TPGParser;
using MGFXC.Framework.Utilities;
using SharpDX;
using SharpDX.D3DCompiler;

namespace MGFXC.Effect;

internal class EffectObject
{
	public enum D3DRENDERSTATETYPE
	{
		ZENABLE = 7,
		FILLMODE = 8,
		SHADEMODE = 9,
		ZWRITEENABLE = 14,
		ALPHATESTENABLE = 15,
		LASTPIXEL = 16,
		SRCBLEND = 19,
		DESTBLEND = 20,
		CULLMODE = 22,
		ZFUNC = 23,
		ALPHAREF = 24,
		ALPHAFUNC = 25,
		DITHERENABLE = 26,
		ALPHABLENDENABLE = 27,
		FOGENABLE = 28,
		SPECULARENABLE = 29,
		FOGCOLOR = 34,
		FOGTABLEMODE = 35,
		FOGSTART = 36,
		FOGEND = 37,
		FOGDENSITY = 38,
		RANGEFOGENABLE = 48,
		STENCILENABLE = 52,
		STENCILFAIL = 53,
		STENCILZFAIL = 54,
		STENCILPASS = 55,
		STENCILFUNC = 56,
		STENCILREF = 57,
		STENCILMASK = 58,
		STENCILWRITEMASK = 59,
		TEXTUREFACTOR = 60,
		WRAP0 = 128,
		WRAP1 = 129,
		WRAP2 = 130,
		WRAP3 = 131,
		WRAP4 = 132,
		WRAP5 = 133,
		WRAP6 = 134,
		WRAP7 = 135,
		CLIPPING = 136,
		LIGHTING = 137,
		AMBIENT = 139,
		FOGVERTEXMODE = 140,
		COLORVERTEX = 141,
		LOCALVIEWER = 142,
		NORMALIZENORMALS = 143,
		DIFFUSEMATERIALSOURCE = 145,
		SPECULARMATERIALSOURCE = 146,
		AMBIENTMATERIALSOURCE = 147,
		EMISSIVEMATERIALSOURCE = 148,
		VERTEXBLEND = 151,
		CLIPPLANEENABLE = 152,
		POINTSIZE = 154,
		POINTSIZE_MIN = 155,
		POINTSPRITEENABLE = 156,
		POINTSCALEENABLE = 157,
		POINTSCALE_A = 158,
		POINTSCALE_B = 159,
		POINTSCALE_C = 160,
		MULTISAMPLEANTIALIAS = 161,
		MULTISAMPLEMASK = 162,
		PATCHEDGESTYLE = 163,
		DEBUGMONITORTOKEN = 165,
		POINTSIZE_MAX = 166,
		INDEXEDVERTEXBLENDENABLE = 167,
		COLORWRITEENABLE = 168,
		TWEENFACTOR = 170,
		BLENDOP = 171,
		POSITIONDEGREE = 172,
		NORMALDEGREE = 173,
		SCISSORTESTENABLE = 174,
		SLOPESCALEDEPTHBIAS = 175,
		ANTIALIASEDLINEENABLE = 176,
		MINTESSELLATIONLEVEL = 178,
		MAXTESSELLATIONLEVEL = 179,
		ADAPTIVETESS_X = 180,
		ADAPTIVETESS_Y = 181,
		ADAPTIVETESS_Z = 182,
		ADAPTIVETESS_W = 183,
		ENABLEADAPTIVETESSELLATION = 184,
		TWOSIDEDSTENCILMODE = 185,
		CCW_STENCILFAIL = 186,
		CCW_STENCILZFAIL = 187,
		CCW_STENCILPASS = 188,
		CCW_STENCILFUNC = 189,
		COLORWRITEENABLE1 = 190,
		COLORWRITEENABLE2 = 191,
		COLORWRITEENABLE3 = 192,
		BLENDFACTOR = 193,
		SRGBWRITEENABLE = 194,
		DEPTHBIAS = 195,
		WRAP8 = 198,
		WRAP9 = 199,
		WRAP10 = 200,
		WRAP11 = 201,
		WRAP12 = 202,
		WRAP13 = 203,
		WRAP14 = 204,
		WRAP15 = 205,
		SEPARATEALPHABLENDENABLE = 206,
		SRCBLENDALPHA = 207,
		DESTBLENDALPHA = 208,
		BLENDOPALPHA = 209,
		FORCE_DWORD = int.MaxValue
	}

	public enum D3DTEXTURESTAGESTATETYPE
	{
		COLOROP = 1,
		COLORARG1 = 2,
		COLORARG2 = 3,
		ALPHAOP = 4,
		ALPHAARG1 = 5,
		ALPHAARG2 = 6,
		BUMPENVMAT00 = 7,
		BUMPENVMAT01 = 8,
		BUMPENVMAT10 = 9,
		BUMPENVMAT11 = 10,
		TEXCOORDINDEX = 11,
		BUMPENVLSCALE = 22,
		BUMPENVLOFFSET = 23,
		TEXTURETRANSFORMFLAGS = 24,
		COLORARG0 = 26,
		ALPHAARG0 = 27,
		RESULTARG = 28,
		CONSTANT = 32,
		FORCE_DWORD = int.MaxValue
	}

	public enum D3DTRANSFORMSTATETYPE
	{
		VIEW = 2,
		PROJECTION = 3,
		TEXTURE0 = 16,
		TEXTURE1 = 17,
		TEXTURE2 = 18,
		TEXTURE3 = 19,
		TEXTURE4 = 20,
		TEXTURE5 = 21,
		TEXTURE6 = 22,
		TEXTURE7 = 23,
		WORLD = 256,
		FORCE_DWORD = int.MaxValue
	}

	public enum D3DXPARAMETER_CLASS
	{
		SCALAR = 0,
		VECTOR = 1,
		MATRIX_ROWS = 2,
		MATRIX_COLUMNS = 3,
		OBJECT = 4,
		STRUCT = 5,
		FORCE_DWORD = int.MaxValue
	}

	public enum D3DXPARAMETER_TYPE
	{
		VOID = 0,
		BOOL = 1,
		INT = 2,
		FLOAT = 3,
		STRING = 4,
		TEXTURE = 5,
		TEXTURE1D = 6,
		TEXTURE2D = 7,
		TEXTURE3D = 8,
		TEXTURECUBE = 9,
		SAMPLER = 10,
		SAMPLER1D = 11,
		SAMPLER2D = 12,
		SAMPLER3D = 13,
		SAMPLERCUBE = 14,
		PIXELSHADER = 15,
		VERTEXSHADER = 16,
		PIXELFRAGMENT = 17,
		VERTEXFRAGMENT = 18,
		UNSUPPORTED = 19,
		FORCE_DWORD = int.MaxValue
	}

	private enum D3DSAMPLERSTATETYPE
	{
		ADDRESSU = 1,
		ADDRESSV = 2,
		ADDRESSW = 3,
		BORDERCOLOR = 4,
		MAGFILTER = 5,
		MINFILTER = 6,
		MIPFILTER = 7,
		MIPMAPLODBIAS = 8,
		MAXMIPLEVEL = 9,
		MAXANISOTROPY = 10,
		SRGBTEXTURE = 11,
		ELEMENTINDEX = 12,
		DMAPOFFSET = 13,
		FORCE_DWORD = int.MaxValue
	}

	public enum STATE_CLASS
	{
		LIGHTENABLE,
		FVF,
		LIGHT,
		MATERIAL,
		NPATCHMODE,
		PIXELSHADER,
		RENDERSTATE,
		SETSAMPLER,
		SAMPLERSTATE,
		TEXTURE,
		TEXTURESTAGE,
		TRANSFORM,
		VERTEXSHADER,
		SHADERCONST,
		UNKNOWN
	}

	public enum MATERIAL_TYPE
	{
		DIFFUSE,
		AMBIENT,
		SPECULAR,
		EMISSIVE,
		POWER
	}

	public enum LIGHT_TYPE
	{
		TYPE,
		DIFFUSE,
		SPECULAR,
		AMBIENT,
		POSITION,
		DIRECTION,
		RANGE,
		FALLOFF,
		ATTENUATION0,
		ATTENUATION1,
		ATTENUATION2,
		THETA,
		PHI
	}

	public enum SHADER_CONSTANT_TYPE
	{
		VSFLOAT,
		VSBOOL,
		VSINT,
		PSFLOAT,
		PSBOOL,
		PSINT
	}

	public enum STATE_TYPE
	{
		CONSTANT,
		PARAMETER,
		EXPRESSION,
		EXPRESSIONINDEX
	}

	public class d3dx_parameter
	{
		public string name;

		public string semantic;

		public object data;

		public D3DXPARAMETER_CLASS class_;

		public D3DXPARAMETER_TYPE type;

		public uint rows;

		public uint columns;

		public uint element_count;

		public uint annotation_count;

		public uint member_count;

		public uint flags;

		public uint bytes;

		public int bufferIndex = -1;

		public int bufferOffset = -1;

		public d3dx_parameter[] annotation_handles;

		public d3dx_parameter[] member_handles;

		public override string ToString()
		{
			if (rows != 0 || columns != 0)
			{
				return $"{class_} {type}{rows}x{columns} {name} : cb{bufferIndex},{bufferOffset}";
			}
			return $"{class_} {type} {name}";
		}
	}

	public class d3dx_state
	{
		public uint operation;

		public uint index;

		public STATE_TYPE type;

		public d3dx_parameter parameter;
	}

	public class d3dx_sampler
	{
		public uint state_count;

		public d3dx_state[] states;
	}

	public class d3dx_pass
	{
		public string name;

		public uint state_count;

		public uint annotation_count;

		public BlendState blendState;

		public DepthStencilState depthStencilState;

		public RasterizerState rasterizerState;

		public d3dx_state[] states;

		public d3dx_parameter[] annotation_handles;
	}

	public class d3dx_technique
	{
		public string name;

		public uint pass_count;

		public uint annotation_count;

		public d3dx_parameter[] annotation_handles;

		public d3dx_pass[] pass_handles;
	}

	public class state_info
	{
		public STATE_CLASS class_ { get; private set; }

		public uint op { get; private set; }

		public string name { get; private set; }

		public state_info(STATE_CLASS class_, uint op, string name)
		{
			this.class_ = class_;
			this.op = op;
			this.name = name;
		}
	}

	public const int D3DX_PARAMETER_SHARED = 1;

	public const int D3DX_PARAMETER_LITERAL = 2;

	public const int D3DX_PARAMETER_ANNOTATION = 4;

	public static readonly state_info[] state_table = new state_info[179]
	{
		new state_info(STATE_CLASS.RENDERSTATE, 7u, "ZENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 8u, "FILLMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 9u, "SHADEMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 14u, "ZWRITEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 15u, "ALPHATESTENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 16u, "LASTPIXEL"),
		new state_info(STATE_CLASS.RENDERSTATE, 19u, "SRCBLEND"),
		new state_info(STATE_CLASS.RENDERSTATE, 20u, "DESTBLEND"),
		new state_info(STATE_CLASS.RENDERSTATE, 22u, "CULLMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 23u, "ZFUNC"),
		new state_info(STATE_CLASS.RENDERSTATE, 24u, "ALPHAREF"),
		new state_info(STATE_CLASS.RENDERSTATE, 25u, "ALPHAFUNC"),
		new state_info(STATE_CLASS.RENDERSTATE, 26u, "DITHERENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 27u, "ALPHABLENDENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 28u, "FOGENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 29u, "SPECULARENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 34u, "FOGCOLOR"),
		new state_info(STATE_CLASS.RENDERSTATE, 35u, "FOGTABLEMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 36u, "FOGSTART"),
		new state_info(STATE_CLASS.RENDERSTATE, 37u, "FOGEND"),
		new state_info(STATE_CLASS.RENDERSTATE, 38u, "FOGDENSITY"),
		new state_info(STATE_CLASS.RENDERSTATE, 48u, "RANGEFOGENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 52u, "STENCILENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 53u, "STENCILFAIL"),
		new state_info(STATE_CLASS.RENDERSTATE, 54u, "STENCILZFAIL"),
		new state_info(STATE_CLASS.RENDERSTATE, 55u, "STENCILPASS"),
		new state_info(STATE_CLASS.RENDERSTATE, 56u, "STENCILFUNC"),
		new state_info(STATE_CLASS.RENDERSTATE, 57u, "STENCILREF"),
		new state_info(STATE_CLASS.RENDERSTATE, 58u, "STENCILMASK"),
		new state_info(STATE_CLASS.RENDERSTATE, 59u, "STENCILWRITEMASK"),
		new state_info(STATE_CLASS.RENDERSTATE, 60u, "TEXTUREFACTOR"),
		new state_info(STATE_CLASS.RENDERSTATE, 128u, "WRAP0"),
		new state_info(STATE_CLASS.RENDERSTATE, 129u, "WRAP1"),
		new state_info(STATE_CLASS.RENDERSTATE, 130u, "WRAP2"),
		new state_info(STATE_CLASS.RENDERSTATE, 131u, "WRAP3"),
		new state_info(STATE_CLASS.RENDERSTATE, 132u, "WRAP4"),
		new state_info(STATE_CLASS.RENDERSTATE, 133u, "WRAP5"),
		new state_info(STATE_CLASS.RENDERSTATE, 134u, "WRAP6"),
		new state_info(STATE_CLASS.RENDERSTATE, 135u, "WRAP7"),
		new state_info(STATE_CLASS.RENDERSTATE, 198u, "WRAP8"),
		new state_info(STATE_CLASS.RENDERSTATE, 199u, "WRAP9"),
		new state_info(STATE_CLASS.RENDERSTATE, 200u, "WRAP10"),
		new state_info(STATE_CLASS.RENDERSTATE, 201u, "WRAP11"),
		new state_info(STATE_CLASS.RENDERSTATE, 202u, "WRAP12"),
		new state_info(STATE_CLASS.RENDERSTATE, 203u, "WRAP13"),
		new state_info(STATE_CLASS.RENDERSTATE, 204u, "WRAP14"),
		new state_info(STATE_CLASS.RENDERSTATE, 205u, "WRAP15"),
		new state_info(STATE_CLASS.RENDERSTATE, 136u, "CLIPPING"),
		new state_info(STATE_CLASS.RENDERSTATE, 137u, "LIGHTING"),
		new state_info(STATE_CLASS.RENDERSTATE, 139u, "AMBIENT"),
		new state_info(STATE_CLASS.RENDERSTATE, 140u, "FOGVERTEXMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 141u, "COLORVERTEX"),
		new state_info(STATE_CLASS.RENDERSTATE, 142u, "LOCALVIEWER"),
		new state_info(STATE_CLASS.RENDERSTATE, 143u, "NORMALIZENORMALS"),
		new state_info(STATE_CLASS.RENDERSTATE, 145u, "DIFFUSEMATERIALSOURCE"),
		new state_info(STATE_CLASS.RENDERSTATE, 146u, "SPECULARMATERIALSOURCE"),
		new state_info(STATE_CLASS.RENDERSTATE, 147u, "AMBIENTMATERIALSOURCE"),
		new state_info(STATE_CLASS.RENDERSTATE, 148u, "EMISSIVEMATERIALSOURCE"),
		new state_info(STATE_CLASS.RENDERSTATE, 151u, "VERTEXBLEND"),
		new state_info(STATE_CLASS.RENDERSTATE, 152u, "CLIPPLANEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 154u, "POINTSIZE"),
		new state_info(STATE_CLASS.RENDERSTATE, 155u, "POINTSIZE_MIN"),
		new state_info(STATE_CLASS.RENDERSTATE, 166u, "POINTSIZE_MAX"),
		new state_info(STATE_CLASS.RENDERSTATE, 156u, "POINTSPRITEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 157u, "POINTSCALEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 158u, "POINTSCALE_A"),
		new state_info(STATE_CLASS.RENDERSTATE, 159u, "POINTSCALE_B"),
		new state_info(STATE_CLASS.RENDERSTATE, 160u, "POINTSCALE_C"),
		new state_info(STATE_CLASS.RENDERSTATE, 161u, "MULTISAMPLEANTIALIAS"),
		new state_info(STATE_CLASS.RENDERSTATE, 162u, "MULTISAMPLEMASK"),
		new state_info(STATE_CLASS.RENDERSTATE, 163u, "PATCHEDGESTYLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 165u, "DEBUGMONITORTOKEN"),
		new state_info(STATE_CLASS.RENDERSTATE, 167u, "INDEXEDVERTEXBLENDENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 168u, "COLORWRITEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 170u, "TWEENFACTOR"),
		new state_info(STATE_CLASS.RENDERSTATE, 171u, "BLENDOP"),
		new state_info(STATE_CLASS.RENDERSTATE, 172u, "POSITIONDEGREE"),
		new state_info(STATE_CLASS.RENDERSTATE, 173u, "NORMALDEGREE"),
		new state_info(STATE_CLASS.RENDERSTATE, 174u, "SCISSORTESTENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 175u, "SLOPESCALEDEPTHBIAS"),
		new state_info(STATE_CLASS.RENDERSTATE, 176u, "ANTIALIASEDLINEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 178u, "MINTESSELLATIONLEVEL"),
		new state_info(STATE_CLASS.RENDERSTATE, 179u, "MAXTESSELLATIONLEVEL"),
		new state_info(STATE_CLASS.RENDERSTATE, 180u, "ADAPTIVETESS_X"),
		new state_info(STATE_CLASS.RENDERSTATE, 181u, "ADAPTIVETESS_Y"),
		new state_info(STATE_CLASS.RENDERSTATE, 182u, "ADAPTIVETESS_Z"),
		new state_info(STATE_CLASS.RENDERSTATE, 183u, "ADAPTIVETESS_W"),
		new state_info(STATE_CLASS.RENDERSTATE, 184u, "ENABLEADAPTIVETESSELLATION"),
		new state_info(STATE_CLASS.RENDERSTATE, 185u, "TWOSIDEDSTENCILMODE"),
		new state_info(STATE_CLASS.RENDERSTATE, 186u, "CCW_STENCILFAIL"),
		new state_info(STATE_CLASS.RENDERSTATE, 187u, "CCW_STENCILZFAIL"),
		new state_info(STATE_CLASS.RENDERSTATE, 188u, "CCW_STENCILPASS"),
		new state_info(STATE_CLASS.RENDERSTATE, 189u, "CCW_STENCILFUNC"),
		new state_info(STATE_CLASS.RENDERSTATE, 190u, "COLORWRITEENABLE1"),
		new state_info(STATE_CLASS.RENDERSTATE, 191u, "COLORWRITEENABLE2"),
		new state_info(STATE_CLASS.RENDERSTATE, 192u, "COLORWRITEENABLE3"),
		new state_info(STATE_CLASS.RENDERSTATE, 193u, "BLENDFACTOR"),
		new state_info(STATE_CLASS.RENDERSTATE, 194u, "SRGBWRITEENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 195u, "DEPTHBIAS"),
		new state_info(STATE_CLASS.RENDERSTATE, 206u, "SEPARATEALPHABLENDENABLE"),
		new state_info(STATE_CLASS.RENDERSTATE, 207u, "SRCBLENDALPHA"),
		new state_info(STATE_CLASS.RENDERSTATE, 208u, "DESTBLENDALPHA"),
		new state_info(STATE_CLASS.RENDERSTATE, 209u, "BLENDOPALPHA"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 1u, "COLOROP"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 26u, "COLORARG0"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 2u, "COLORARG1"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 3u, "COLORARG2"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 4u, "ALPHAOP"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 27u, "ALPHAARG0"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 5u, "ALPHAARG1"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 6u, "ALPHAARG2"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 28u, "RESULTARG"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 7u, "BUMPENVMAT00"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 8u, "BUMPENVMAT01"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 9u, "BUMPENVMAT10"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 10u, "BUMPENVMAT11"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 11u, "TEXCOORDINDEX"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 22u, "BUMPENVLSCALE"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 23u, "BUMPENVLOFFSET"),
		new state_info(STATE_CLASS.TEXTURESTAGE, 24u, "TEXTURETRANSFORMFLAGS"),
		new state_info(STATE_CLASS.UNKNOWN, 0u, "UNKNOWN"),
		new state_info(STATE_CLASS.NPATCHMODE, 0u, "NPatchMode"),
		new state_info(STATE_CLASS.UNKNOWN, 0u, "UNKNOWN"),
		new state_info(STATE_CLASS.TRANSFORM, 3u, "PROJECTION"),
		new state_info(STATE_CLASS.TRANSFORM, 2u, "VIEW"),
		new state_info(STATE_CLASS.TRANSFORM, 256u, "WORLD"),
		new state_info(STATE_CLASS.TRANSFORM, 16u, "TEXTURE0"),
		new state_info(STATE_CLASS.MATERIAL, 0u, "MaterialDiffuse"),
		new state_info(STATE_CLASS.MATERIAL, 1u, "MaterialAmbient"),
		new state_info(STATE_CLASS.MATERIAL, 2u, "MaterialSpecular"),
		new state_info(STATE_CLASS.MATERIAL, 3u, "MaterialEmissive"),
		new state_info(STATE_CLASS.MATERIAL, 4u, "MaterialPower"),
		new state_info(STATE_CLASS.LIGHT, 0u, "LightType"),
		new state_info(STATE_CLASS.LIGHT, 1u, "LightDiffuse"),
		new state_info(STATE_CLASS.LIGHT, 2u, "LightSpecular"),
		new state_info(STATE_CLASS.LIGHT, 3u, "LightAmbient"),
		new state_info(STATE_CLASS.LIGHT, 4u, "LightPosition"),
		new state_info(STATE_CLASS.LIGHT, 5u, "LightDirection"),
		new state_info(STATE_CLASS.LIGHT, 6u, "LightRange"),
		new state_info(STATE_CLASS.LIGHT, 7u, "LightFallOff"),
		new state_info(STATE_CLASS.LIGHT, 8u, "LightAttenuation0"),
		new state_info(STATE_CLASS.LIGHT, 9u, "LightAttenuation1"),
		new state_info(STATE_CLASS.LIGHT, 10u, "LightAttenuation2"),
		new state_info(STATE_CLASS.LIGHT, 11u, "LightTheta"),
		new state_info(STATE_CLASS.LIGHT, 12u, "LightPhi"),
		new state_info(STATE_CLASS.LIGHTENABLE, 0u, "LightEnable"),
		new state_info(STATE_CLASS.VERTEXSHADER, 0u, "Vertexshader"),
		new state_info(STATE_CLASS.PIXELSHADER, 0u, "Pixelshader"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstantF"),
		new state_info(STATE_CLASS.SHADERCONST, 1u, "VertexShaderConstantB"),
		new state_info(STATE_CLASS.SHADERCONST, 2u, "VertexShaderConstantI"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstant"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstant1"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstant2"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstant3"),
		new state_info(STATE_CLASS.SHADERCONST, 0u, "VertexShaderConstant4"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstantF"),
		new state_info(STATE_CLASS.SHADERCONST, 4u, "PixelShaderConstantB"),
		new state_info(STATE_CLASS.SHADERCONST, 5u, "PixelShaderConstantI"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstant"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstant1"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstant2"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstant3"),
		new state_info(STATE_CLASS.SHADERCONST, 3u, "PixelShaderConstant4"),
		new state_info(STATE_CLASS.TEXTURE, 0u, "Texture"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 1u, "AddressU"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 2u, "AddressV"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 3u, "AddressW"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 4u, "BorderColor"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 5u, "MagFilter"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 6u, "MinFilter"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 7u, "MipFilter"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 8u, "MipMapLodBias"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 9u, "MaxMipLevel"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 10u, "MaxAnisotropy"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 11u, "SRGBTexture"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 12u, "ElementIndex"),
		new state_info(STATE_CLASS.SAMPLERSTATE, 13u, "DMAPOffset"),
		new state_info(STATE_CLASS.SETSAMPLER, 0u, "Sampler")
	};

	private const string Header = "MGFX";

	private const int Version = 9;

	public d3dx_parameter[] Objects { get; private set; }

	public d3dx_parameter[] Parameters { get; private set; }

	public d3dx_technique[] Techniques { get; private set; }

	public List<ShaderData> Shaders { get; private set; }

	public List<ConstantBufferData> ConstantBuffers { get; private set; }

	private EffectObject()
	{
	}

	public static EffectParameterClass ToXNAParameterClass(D3DXPARAMETER_CLASS class_)
	{
		switch (class_)
		{
		case D3DXPARAMETER_CLASS.SCALAR:
			return EffectParameterClass.Scalar;
		case D3DXPARAMETER_CLASS.VECTOR:
			return EffectParameterClass.Vector;
		case D3DXPARAMETER_CLASS.MATRIX_ROWS:
		case D3DXPARAMETER_CLASS.MATRIX_COLUMNS:
			return EffectParameterClass.Matrix;
		case D3DXPARAMETER_CLASS.OBJECT:
			return EffectParameterClass.Object;
		case D3DXPARAMETER_CLASS.STRUCT:
			return EffectParameterClass.Struct;
		default:
			throw new NotImplementedException();
		}
	}

	public static EffectParameterType ToXNAParameterType(D3DXPARAMETER_TYPE type)
	{
		return type switch
		{
			D3DXPARAMETER_TYPE.BOOL => EffectParameterType.Bool, 
			D3DXPARAMETER_TYPE.INT => EffectParameterType.Int32, 
			D3DXPARAMETER_TYPE.FLOAT => EffectParameterType.Single, 
			D3DXPARAMETER_TYPE.STRING => EffectParameterType.String, 
			D3DXPARAMETER_TYPE.TEXTURE => EffectParameterType.Texture, 
			D3DXPARAMETER_TYPE.TEXTURE1D => EffectParameterType.Texture1D, 
			D3DXPARAMETER_TYPE.TEXTURE2D => EffectParameterType.Texture2D, 
			D3DXPARAMETER_TYPE.TEXTURE3D => EffectParameterType.Texture3D, 
			D3DXPARAMETER_TYPE.TEXTURECUBE => EffectParameterType.TextureCube, 
			_ => throw new NotImplementedException(), 
		};
	}

	internal static VertexElementUsage ToXNAVertexElementUsage(MojoShader.MOJOSHADER_usage usage)
	{
		return usage switch
		{
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_POSITION => VertexElementUsage.Position, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_BLENDWEIGHT => VertexElementUsage.BlendWeight, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_BLENDINDICES => VertexElementUsage.BlendIndices, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_NORMAL => VertexElementUsage.Normal, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_POINTSIZE => VertexElementUsage.PointSize, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_TEXCOORD => VertexElementUsage.TextureCoordinate, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_TANGENT => VertexElementUsage.Tangent, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_BINORMAL => VertexElementUsage.Binormal, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_TESSFACTOR => VertexElementUsage.TessellateFactor, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_COLOR => VertexElementUsage.Color, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_FOG => VertexElementUsage.Fog, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_DEPTH => VertexElementUsage.Depth, 
			MojoShader.MOJOSHADER_usage.MOJOSHADER_USAGE_SAMPLE => VertexElementUsage.Sample, 
			_ => throw new NotImplementedException(), 
		};
	}

	public static EffectObject CompileEffect(ShaderResult shaderResult, out string errorsAndWarnings)
	{
		EffectObject effect = new EffectObject();
		errorsAndWarnings = string.Empty;
		effect.ConstantBuffers = new List<ConstantBufferData>();
		effect.Shaders = new List<ShaderData>();
		ShaderInfo shaderInfo = shaderResult.ShaderInfo;
		effect.Techniques = new d3dx_technique[shaderInfo.Techniques.Count];
		for (int t = 0; t < shaderInfo.Techniques.Count; t++)
		{
			TechniqueInfo tinfo = shaderInfo.Techniques[t];
			d3dx_technique technique = new d3dx_technique();
			technique.name = tinfo.name;
			technique.pass_count = (uint)tinfo.Passes.Count;
			technique.pass_handles = new d3dx_pass[tinfo.Passes.Count];
			for (int p = 0; p < tinfo.Passes.Count; p++)
			{
				PassInfo pinfo = tinfo.Passes[p];
				d3dx_pass pass = new d3dx_pass();
				pass.name = pinfo.name ?? string.Empty;
				pass.blendState = pinfo.blendState;
				pass.depthStencilState = pinfo.depthStencilState;
				pass.rasterizerState = pinfo.rasterizerState;
				pass.state_count = 0u;
				d3dx_state[] tempstate = new d3dx_state[2];
				shaderResult.Profile.ValidateShaderModels(pinfo);
				if (!string.IsNullOrEmpty(pinfo.psFunction))
				{
					pass.state_count++;
					tempstate[pass.state_count - 1] = effect.CreateShader(shaderResult, pinfo.psFunction, pinfo.psModel, isVertexShader: false, ref errorsAndWarnings);
				}
				if (!string.IsNullOrEmpty(pinfo.vsFunction))
				{
					pass.state_count++;
					tempstate[pass.state_count - 1] = effect.CreateShader(shaderResult, pinfo.vsFunction, pinfo.vsModel, isVertexShader: true, ref errorsAndWarnings);
				}
				pass.states = new d3dx_state[pass.state_count];
				for (int s = 0; s < pass.state_count; s++)
				{
					pass.states[s] = tempstate[s];
				}
				technique.pass_handles[p] = pass;
			}
			effect.Techniques[t] = technique;
		}
		List<d3dx_parameter> parameters = new List<d3dx_parameter>();
		for (int c = 0; c < effect.ConstantBuffers.Count; c++)
		{
			ConstantBufferData cb = effect.ConstantBuffers[c];
			for (int i = 0; i < cb.Parameters.Count; i++)
			{
				d3dx_parameter param2 = cb.Parameters[i];
				int match = parameters.FindIndex((d3dx_parameter e) => e.name == param2.name);
				if (match == -1)
				{
					cb.ParameterIndex.Add(parameters.Count);
					parameters.Add(param2);
				}
				else
				{
					cb.ParameterIndex.Add(match);
				}
			}
		}
		foreach (ShaderData shader in effect.Shaders)
		{
			for (int s2 = 0; s2 < shader._samplers.Length; s2++)
			{
				ShaderData.Sampler sampler = shader._samplers[s2];
				int match2 = parameters.FindIndex((d3dx_parameter e) => e.name == sampler.parameterName);
				if (match2 == -1)
				{
					shader._samplers[s2].parameter = parameters.Count;
					d3dx_parameter param = new d3dx_parameter();
					param.class_ = D3DXPARAMETER_CLASS.OBJECT;
					param.name = sampler.parameterName;
					param.semantic = string.Empty;
					switch (sampler.type)
					{
					case MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_1D:
						param.type = D3DXPARAMETER_TYPE.TEXTURE1D;
						break;
					case MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_2D:
						param.type = D3DXPARAMETER_TYPE.TEXTURE2D;
						break;
					case MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_VOLUME:
						param.type = D3DXPARAMETER_TYPE.TEXTURE3D;
						break;
					case MojoShader.MOJOSHADER_samplerType.MOJOSHADER_SAMPLER_CUBE:
						param.type = D3DXPARAMETER_TYPE.TEXTURECUBE;
						break;
					}
					parameters.Add(param);
				}
				else
				{
					shader._samplers[s2].parameter = match2;
				}
			}
		}
		effect.Parameters = parameters.ToArray();
		return effect;
	}

	private d3dx_state CreateShader(ShaderResult shaderResult, string shaderFunction, string shaderProfile, bool isVertexShader, ref string errorsAndWarnings)
	{
		ShaderData shaderData = shaderResult.Profile.CreateShader(shaderResult, shaderFunction, shaderProfile, isVertexShader, this, ref errorsAndWarnings);
		d3dx_state d3dx_state = new d3dx_state();
		d3dx_state.index = 0u;
		d3dx_state.type = STATE_TYPE.CONSTANT;
		d3dx_state.operation = (isVertexShader ? 146u : 147u);
		d3dx_state.parameter = new d3dx_parameter();
		d3dx_state.parameter.name = string.Empty;
		d3dx_state.parameter.semantic = string.Empty;
		d3dx_state.parameter.class_ = D3DXPARAMETER_CLASS.OBJECT;
		d3dx_state.parameter.type = (isVertexShader ? D3DXPARAMETER_TYPE.VERTEXSHADER : D3DXPARAMETER_TYPE.PIXELSHADER);
		d3dx_state.parameter.rows = 0u;
		d3dx_state.parameter.columns = 0u;
		d3dx_state.parameter.data = shaderData.SharedIndex;
		return d3dx_state;
	}

	internal static int GetShaderIndex(STATE_CLASS type, d3dx_state[] states)
	{
		foreach (d3dx_state state in states)
		{
			if (state_table[state.operation].class_ == type)
			{
				if (state.type != 0)
				{
					throw new NotSupportedException("We do not support shader expressions!");
				}
				return (int)state.parameter.data;
			}
		}
		return -1;
	}

	public static byte[] CompileHLSL(ShaderResult shaderResult, string shaderFunction, string shaderProfile, ref string errorsAndWarnings)
	{
		ShaderBytecode shaderByteCode;
		try
		{
			ShaderFlags shaderFlags = ShaderFlags.OptimizationLevel1;
			if (shaderResult.Profile == ShaderProfile.DirectX_11)
			{
				shaderFlags |= ShaderFlags.EnableBackwardsCompatibility;
			}
			if (shaderResult.Debug)
			{
				shaderFlags |= ShaderFlags.SkipOptimization;
				shaderFlags |= ShaderFlags.Debug;
			}
			else
			{
				shaderFlags |= ShaderFlags.OptimizationLevel3;
			}
			CompilationResult result = ShaderBytecode.Compile(shaderResult.FileContent, shaderFunction, shaderProfile, shaderFlags, EffectFlags.None, null, null, shaderResult.FilePath);
			errorsAndWarnings += result.Message;
			if (result.Bytecode == null)
			{
				throw new ShaderCompilerException();
			}
			shaderByteCode = result.Bytecode;
		}
		catch (CompilationException ex)
		{
			errorsAndWarnings += ex.Message;
			throw new ShaderCompilerException();
		}
		return shaderByteCode.Data.ToArray();
	}

	private static byte[] CompilePSSL(ShaderResult shaderResult, string shaderFunction, string shaderProfile, ref string errorsAndWarnings)
	{
		throw new NotImplementedException();
	}

	public void Write(BinaryWriter writer, Options options)
	{
		writer.Write("MGFX".ToCharArray());
		writer.Write((byte)9);
		byte profile = options.Profile.FormatId;
		writer.Write(profile);
		using MemoryStream memStream = new MemoryStream();
		using BinaryWriterEx memWriter = new BinaryWriterEx(memStream);
		memWriter.Write((byte)ConstantBuffers.Count);
		foreach (ConstantBufferData constantBuffer in ConstantBuffers)
		{
			constantBuffer.Write(memWriter, options);
		}
		memWriter.Write((byte)Shaders.Count);
		foreach (ShaderData shader in Shaders)
		{
			shader.Write(memWriter, options);
		}
		WriteParameters(memWriter, Parameters, Parameters.Length);
		memWriter.Write((byte)Techniques.Length);
		d3dx_technique[] techniques = Techniques;
		foreach (d3dx_technique technique in techniques)
		{
			memWriter.Write(technique.name);
			WriteAnnotations(memWriter, technique.annotation_handles);
			memWriter.Write((byte)technique.pass_count);
			for (int p = 0; p < technique.pass_count; p++)
			{
				d3dx_pass pass = technique.pass_handles[p];
				memWriter.Write(pass.name);
				WriteAnnotations(memWriter, pass.annotation_handles);
				int vertexShader = GetShaderIndex(STATE_CLASS.VERTEXSHADER, pass.states);
				int pixelShader = GetShaderIndex(STATE_CLASS.PIXELSHADER, pass.states);
				memWriter.Write((byte)vertexShader);
				memWriter.Write((byte)pixelShader);
				if (pass.blendState != null)
				{
					memWriter.Write(value: true);
					memWriter.Write((byte)pass.blendState.AlphaBlendFunction);
					memWriter.Write((byte)pass.blendState.AlphaDestinationBlend);
					memWriter.Write((byte)pass.blendState.AlphaSourceBlend);
					memWriter.Write(pass.blendState.BlendFactor.R);
					memWriter.Write(pass.blendState.BlendFactor.G);
					memWriter.Write(pass.blendState.BlendFactor.B);
					memWriter.Write(pass.blendState.BlendFactor.A);
					memWriter.Write((byte)pass.blendState.ColorBlendFunction);
					memWriter.Write((byte)pass.blendState.ColorDestinationBlend);
					memWriter.Write((byte)pass.blendState.ColorSourceBlend);
					memWriter.Write((byte)pass.blendState.ColorWriteChannels);
					memWriter.Write((byte)pass.blendState.ColorWriteChannels1);
					memWriter.Write((byte)pass.blendState.ColorWriteChannels2);
					memWriter.Write((byte)pass.blendState.ColorWriteChannels3);
					memWriter.Write(pass.blendState.MultiSampleMask);
				}
				else
				{
					memWriter.Write(value: false);
				}
				if (pass.depthStencilState != null)
				{
					memWriter.Write(value: true);
					memWriter.Write((byte)pass.depthStencilState.CounterClockwiseStencilDepthBufferFail);
					memWriter.Write((byte)pass.depthStencilState.CounterClockwiseStencilFail);
					memWriter.Write((byte)pass.depthStencilState.CounterClockwiseStencilFunction);
					memWriter.Write((byte)pass.depthStencilState.CounterClockwiseStencilPass);
					memWriter.Write(pass.depthStencilState.DepthBufferEnable);
					memWriter.Write((byte)pass.depthStencilState.DepthBufferFunction);
					memWriter.Write(pass.depthStencilState.DepthBufferWriteEnable);
					memWriter.Write(pass.depthStencilState.ReferenceStencil);
					memWriter.Write((byte)pass.depthStencilState.StencilDepthBufferFail);
					memWriter.Write(pass.depthStencilState.StencilEnable);
					memWriter.Write((byte)pass.depthStencilState.StencilFail);
					memWriter.Write((byte)pass.depthStencilState.StencilFunction);
					memWriter.Write(pass.depthStencilState.StencilMask);
					memWriter.Write((byte)pass.depthStencilState.StencilPass);
					memWriter.Write(pass.depthStencilState.StencilWriteMask);
					memWriter.Write(pass.depthStencilState.TwoSidedStencilMode);
				}
				else
				{
					memWriter.Write(value: false);
				}
				if (pass.rasterizerState != null)
				{
					memWriter.Write(value: true);
					memWriter.Write((byte)pass.rasterizerState.CullMode);
					memWriter.Write(pass.rasterizerState.DepthBias);
					memWriter.Write((byte)pass.rasterizerState.FillMode);
					memWriter.Write(pass.rasterizerState.MultiSampleAntiAlias);
					memWriter.Write(pass.rasterizerState.ScissorTestEnable);
					memWriter.Write(pass.rasterizerState.SlopeScaleDepthBias);
				}
				else
				{
					memWriter.Write(value: false);
				}
			}
		}
		int effectKey = Hash.ComputeHash(memStream);
		writer.Write(effectKey);
		memStream.WriteTo(writer.BaseStream);
	}

	private static void WriteParameters(BinaryWriterEx writer, d3dx_parameter[] parameters, int count)
	{
		writer.Write7BitEncodedInt(count);
		for (int i = 0; i < count; i++)
		{
			WriteParameter(writer, parameters[i]);
		}
	}

	private static void WriteParameter(BinaryWriterEx writer, d3dx_parameter param)
	{
		EffectParameterClass class_ = ToXNAParameterClass(param.class_);
		EffectParameterType type = ToXNAParameterType(param.type);
		writer.Write((byte)class_);
		writer.Write((byte)type);
		writer.Write(param.name);
		writer.Write(param.semantic);
		WriteAnnotations(writer, param.annotation_handles);
		writer.Write((byte)param.rows);
		writer.Write((byte)param.columns);
		WriteParameters(writer, param.member_handles, (int)param.element_count);
		WriteParameters(writer, param.member_handles, (int)param.member_count);
		if (param.element_count == 0 && param.member_count == 0 && (uint)(type - 1) <= 2u)
		{
			writer.Write((byte[])param.data);
		}
	}

	private static void WriteAnnotations(BinaryWriterEx writer, d3dx_parameter[] annotations)
	{
		int count = ((annotations != null) ? annotations.Length : 0);
		writer.Write((byte)count);
		for (int i = 0; i < count; i++)
		{
			WriteParameter(writer, annotations[i]);
		}
	}
}

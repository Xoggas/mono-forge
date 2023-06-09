using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace MGFXC.Effect.TPGParser;

[Serializable]
[XmlInclude(typeof(ParseTree))]
public class ParseNode
{
	protected string text;

	protected List<ParseNode> nodes;

	[XmlIgnore]
	public ParseNode Parent;

	public Token Token;

	public List<ParseNode> Nodes => nodes;

	[XmlIgnore]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	public virtual ParseNode CreateNode(Token token, string text)
	{
		return new ParseNode(token, text)
		{
			Parent = this
		};
	}

	protected ParseNode(Token token, string text)
	{
		Token = token;
		this.text = text;
		nodes = new List<ParseNode>();
	}

	protected object GetValue(ParseTree tree, TokenType type, int index)
	{
		return GetValue(tree, type, ref index);
	}

	protected object GetValue(ParseTree tree, TokenType type, ref int index)
	{
		object o = null;
		if (index < 0)
		{
			return o;
		}
		foreach (ParseNode node in nodes)
		{
			if (node.Token.Type == type)
			{
				index--;
				if (index < 0)
				{
					return node.Eval(tree);
				}
			}
		}
		return o;
	}

	internal object Eval(ParseTree tree, params object[] paramlist)
	{
		object Value = null;
		return Token.Type switch
		{
			TokenType.Start => EvalStart(tree, paramlist), 
			TokenType.Technique_Declaration => EvalTechnique_Declaration(tree, paramlist), 
			TokenType.FillMode_Solid => EvalFillMode_Solid(tree, paramlist), 
			TokenType.FillMode_WireFrame => EvalFillMode_WireFrame(tree, paramlist), 
			TokenType.FillModes => EvalFillModes(tree, paramlist), 
			TokenType.CullMode_None => EvalCullMode_None(tree, paramlist), 
			TokenType.CullMode_Cw => EvalCullMode_Cw(tree, paramlist), 
			TokenType.CullMode_Ccw => EvalCullMode_Ccw(tree, paramlist), 
			TokenType.CullModes => EvalCullModes(tree, paramlist), 
			TokenType.Colors_None => EvalColors_None(tree, paramlist), 
			TokenType.Colors_Red => EvalColors_Red(tree, paramlist), 
			TokenType.Colors_Green => EvalColors_Green(tree, paramlist), 
			TokenType.Colors_Blue => EvalColors_Blue(tree, paramlist), 
			TokenType.Colors_Alpha => EvalColors_Alpha(tree, paramlist), 
			TokenType.Colors_All => EvalColors_All(tree, paramlist), 
			TokenType.Colors_Boolean => EvalColors_Boolean(tree, paramlist), 
			TokenType.Colors => EvalColors(tree, paramlist), 
			TokenType.ColorsMasks => EvalColorsMasks(tree, paramlist), 
			TokenType.Blend_Zero => EvalBlend_Zero(tree, paramlist), 
			TokenType.Blend_One => EvalBlend_One(tree, paramlist), 
			TokenType.Blend_SrcColor => EvalBlend_SrcColor(tree, paramlist), 
			TokenType.Blend_InvSrcColor => EvalBlend_InvSrcColor(tree, paramlist), 
			TokenType.Blend_SrcAlpha => EvalBlend_SrcAlpha(tree, paramlist), 
			TokenType.Blend_InvSrcAlpha => EvalBlend_InvSrcAlpha(tree, paramlist), 
			TokenType.Blend_DestAlpha => EvalBlend_DestAlpha(tree, paramlist), 
			TokenType.Blend_InvDestAlpha => EvalBlend_InvDestAlpha(tree, paramlist), 
			TokenType.Blend_DestColor => EvalBlend_DestColor(tree, paramlist), 
			TokenType.Blend_InvDestColor => EvalBlend_InvDestColor(tree, paramlist), 
			TokenType.Blend_SrcAlphaSat => EvalBlend_SrcAlphaSat(tree, paramlist), 
			TokenType.Blend_BlendFactor => EvalBlend_BlendFactor(tree, paramlist), 
			TokenType.Blend_InvBlendFactor => EvalBlend_InvBlendFactor(tree, paramlist), 
			TokenType.Blends => EvalBlends(tree, paramlist), 
			TokenType.BlendOp_Add => EvalBlendOp_Add(tree, paramlist), 
			TokenType.BlendOp_Subtract => EvalBlendOp_Subtract(tree, paramlist), 
			TokenType.BlendOp_RevSubtract => EvalBlendOp_RevSubtract(tree, paramlist), 
			TokenType.BlendOp_Min => EvalBlendOp_Min(tree, paramlist), 
			TokenType.BlendOp_Max => EvalBlendOp_Max(tree, paramlist), 
			TokenType.BlendOps => EvalBlendOps(tree, paramlist), 
			TokenType.CmpFunc_Never => EvalCmpFunc_Never(tree, paramlist), 
			TokenType.CmpFunc_Less => EvalCmpFunc_Less(tree, paramlist), 
			TokenType.CmpFunc_Equal => EvalCmpFunc_Equal(tree, paramlist), 
			TokenType.CmpFunc_LessEqual => EvalCmpFunc_LessEqual(tree, paramlist), 
			TokenType.CmpFunc_Greater => EvalCmpFunc_Greater(tree, paramlist), 
			TokenType.CmpFunc_NotEqual => EvalCmpFunc_NotEqual(tree, paramlist), 
			TokenType.CmpFunc_GreaterEqual => EvalCmpFunc_GreaterEqual(tree, paramlist), 
			TokenType.CmpFunc_Always => EvalCmpFunc_Always(tree, paramlist), 
			TokenType.CmpFunc => EvalCmpFunc(tree, paramlist), 
			TokenType.StencilOp_Keep => EvalStencilOp_Keep(tree, paramlist), 
			TokenType.StencilOp_Zero => EvalStencilOp_Zero(tree, paramlist), 
			TokenType.StencilOp_Replace => EvalStencilOp_Replace(tree, paramlist), 
			TokenType.StencilOp_IncrSat => EvalStencilOp_IncrSat(tree, paramlist), 
			TokenType.StencilOp_DecrSat => EvalStencilOp_DecrSat(tree, paramlist), 
			TokenType.StencilOp_Invert => EvalStencilOp_Invert(tree, paramlist), 
			TokenType.StencilOp_Incr => EvalStencilOp_Incr(tree, paramlist), 
			TokenType.StencilOp_Decr => EvalStencilOp_Decr(tree, paramlist), 
			TokenType.StencilOp => EvalStencilOp(tree, paramlist), 
			TokenType.Render_State_CullMode => EvalRender_State_CullMode(tree, paramlist), 
			TokenType.Render_State_FillMode => EvalRender_State_FillMode(tree, paramlist), 
			TokenType.Render_State_AlphaBlendEnable => EvalRender_State_AlphaBlendEnable(tree, paramlist), 
			TokenType.Render_State_SrcBlend => EvalRender_State_SrcBlend(tree, paramlist), 
			TokenType.Render_State_DestBlend => EvalRender_State_DestBlend(tree, paramlist), 
			TokenType.Render_State_BlendOp => EvalRender_State_BlendOp(tree, paramlist), 
			TokenType.Render_State_ColorWriteEnable => EvalRender_State_ColorWriteEnable(tree, paramlist), 
			TokenType.Render_State_DepthBias => EvalRender_State_DepthBias(tree, paramlist), 
			TokenType.Render_State_SlopeScaleDepthBias => EvalRender_State_SlopeScaleDepthBias(tree, paramlist), 
			TokenType.Render_State_ZEnable => EvalRender_State_ZEnable(tree, paramlist), 
			TokenType.Render_State_ZWriteEnable => EvalRender_State_ZWriteEnable(tree, paramlist), 
			TokenType.Render_State_ZFunc => EvalRender_State_ZFunc(tree, paramlist), 
			TokenType.Render_State_MultiSampleAntiAlias => EvalRender_State_MultiSampleAntiAlias(tree, paramlist), 
			TokenType.Render_State_ScissorTestEnable => EvalRender_State_ScissorTestEnable(tree, paramlist), 
			TokenType.Render_State_StencilEnable => EvalRender_State_StencilEnable(tree, paramlist), 
			TokenType.Render_State_StencilFail => EvalRender_State_StencilFail(tree, paramlist), 
			TokenType.Render_State_StencilFunc => EvalRender_State_StencilFunc(tree, paramlist), 
			TokenType.Render_State_StencilMask => EvalRender_State_StencilMask(tree, paramlist), 
			TokenType.Render_State_StencilPass => EvalRender_State_StencilPass(tree, paramlist), 
			TokenType.Render_State_StencilRef => EvalRender_State_StencilRef(tree, paramlist), 
			TokenType.Render_State_StencilWriteMask => EvalRender_State_StencilWriteMask(tree, paramlist), 
			TokenType.Render_State_StencilZFail => EvalRender_State_StencilZFail(tree, paramlist), 
			TokenType.Render_State_Expression => EvalRender_State_Expression(tree, paramlist), 
			TokenType.Pass_Declaration => EvalPass_Declaration(tree, paramlist), 
			TokenType.VertexShader_Pass_Expression => EvalVertexShader_Pass_Expression(tree, paramlist), 
			TokenType.PixelShader_Pass_Expression => EvalPixelShader_Pass_Expression(tree, paramlist), 
			TokenType.AddressMode_Clamp => EvalAddressMode_Clamp(tree, paramlist), 
			TokenType.AddressMode_Wrap => EvalAddressMode_Wrap(tree, paramlist), 
			TokenType.AddressMode_Mirror => EvalAddressMode_Mirror(tree, paramlist), 
			TokenType.AddressMode_Border => EvalAddressMode_Border(tree, paramlist), 
			TokenType.AddressMode => EvalAddressMode(tree, paramlist), 
			TokenType.TextureFilter_None => EvalTextureFilter_None(tree, paramlist), 
			TokenType.TextureFilter_Linear => EvalTextureFilter_Linear(tree, paramlist), 
			TokenType.TextureFilter_Point => EvalTextureFilter_Point(tree, paramlist), 
			TokenType.TextureFilter_Anisotropic => EvalTextureFilter_Anisotropic(tree, paramlist), 
			TokenType.TextureFilter => EvalTextureFilter(tree, paramlist), 
			TokenType.Sampler_State_Texture => EvalSampler_State_Texture(tree, paramlist), 
			TokenType.Sampler_State_MinFilter => EvalSampler_State_MinFilter(tree, paramlist), 
			TokenType.Sampler_State_MagFilter => EvalSampler_State_MagFilter(tree, paramlist), 
			TokenType.Sampler_State_MipFilter => EvalSampler_State_MipFilter(tree, paramlist), 
			TokenType.Sampler_State_Filter => EvalSampler_State_Filter(tree, paramlist), 
			TokenType.Sampler_State_AddressU => EvalSampler_State_AddressU(tree, paramlist), 
			TokenType.Sampler_State_AddressV => EvalSampler_State_AddressV(tree, paramlist), 
			TokenType.Sampler_State_AddressW => EvalSampler_State_AddressW(tree, paramlist), 
			TokenType.Sampler_State_BorderColor => EvalSampler_State_BorderColor(tree, paramlist), 
			TokenType.Sampler_State_MaxMipLevel => EvalSampler_State_MaxMipLevel(tree, paramlist), 
			TokenType.Sampler_State_MaxAnisotropy => EvalSampler_State_MaxAnisotropy(tree, paramlist), 
			TokenType.Sampler_State_MipLodBias => EvalSampler_State_MipLodBias(tree, paramlist), 
			TokenType.Sampler_State_Expression => EvalSampler_State_Expression(tree, paramlist), 
			TokenType.Sampler_Register_Expression => EvalSampler_Register_Expression(tree, paramlist), 
			TokenType.Sampler_Declaration_States => EvalSampler_Declaration_States(tree, paramlist), 
			TokenType.Sampler_Declaration => EvalSampler_Declaration(tree, paramlist), 
			_ => Token.Text, 
		};
	}

	protected virtual object EvalStart(ParseTree tree, params object[] paramlist)
	{
		ShaderInfo shader = new ShaderInfo();
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, shader);
		}
		return shader;
	}

	protected virtual object EvalTechnique_Declaration(ParseTree tree, params object[] paramlist)
	{
		TechniqueInfo technique = new TechniqueInfo();
		technique.name = (GetValue(tree, TokenType.Identifier, 0) as string) ?? string.Empty;
		technique.startPos = Token.StartPos;
		technique.length = Token.Length;
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, technique);
		}
		if (technique.Passes.Count > 0)
		{
			(paramlist[0] as ShaderInfo).Techniques.Add(technique);
		}
		return null;
	}

	protected virtual object EvalFillMode_Solid(ParseTree tree, params object[] paramlist)
	{
		return FillMode.Solid;
	}

	protected virtual object EvalFillMode_WireFrame(ParseTree tree, params object[] paramlist)
	{
		return FillMode.WireFrame;
	}

	protected virtual object EvalFillModes(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.FillMode_Solid, 0) ?? GetValue(tree, TokenType.FillMode_WireFrame, 0);
	}

	protected virtual object EvalCullMode_None(ParseTree tree, params object[] paramlist)
	{
		return CullMode.None;
	}

	protected virtual object EvalCullMode_Cw(ParseTree tree, params object[] paramlist)
	{
		return CullMode.CullClockwiseFace;
	}

	protected virtual object EvalCullMode_Ccw(ParseTree tree, params object[] paramlist)
	{
		return CullMode.CullCounterClockwiseFace;
	}

	protected virtual object EvalCullModes(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.CullMode_None, 0) ?? GetValue(tree, TokenType.CullMode_Cw, 0) ?? GetValue(tree, TokenType.CullMode_Ccw, 0);
	}

	protected virtual object EvalColors_None(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.None;
	}

	protected virtual object EvalColors_Red(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.Red;
	}

	protected virtual object EvalColors_Green(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.Green;
	}

	protected virtual object EvalColors_Blue(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.Blue;
	}

	protected virtual object EvalColors_Alpha(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.Alpha;
	}

	protected virtual object EvalColors_All(ParseTree tree, params object[] paramlist)
	{
		return ColorWriteChannels.All;
	}

	protected virtual object EvalColors_Boolean(ParseTree tree, params object[] paramlist)
	{
		return ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0)) ? ColorWriteChannels.All : ColorWriteChannels.None;
	}

	protected virtual object EvalColors(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.Colors_Red, 0) ?? GetValue(tree, TokenType.Colors_Green, 0) ?? GetValue(tree, TokenType.Colors_Blue, 0) ?? GetValue(tree, TokenType.Colors_Alpha, 0) ?? GetValue(tree, TokenType.Colors_None, 0) ?? GetValue(tree, TokenType.Colors_All, 0) ?? GetValue(tree, TokenType.Colors_Boolean, 0);
	}

	protected virtual object EvalColorsMasks(ParseTree tree, params object[] paramlist)
	{
		return (ColorWriteChannels)(GetValue(tree, TokenType.Colors, 0) ?? ((object)0)) | (ColorWriteChannels)(GetValue(tree, TokenType.Colors, 1) ?? ((object)0)) | (ColorWriteChannels)(GetValue(tree, TokenType.Colors, 2) ?? ((object)0)) | (ColorWriteChannels)(GetValue(tree, TokenType.Colors, 3) ?? ((object)0));
	}

	protected virtual object EvalBlend_Zero(ParseTree tree, params object[] paramlist)
	{
		return Blend.Zero;
	}

	protected virtual object EvalBlend_One(ParseTree tree, params object[] paramlist)
	{
		return Blend.One;
	}

	protected virtual object EvalBlend_SrcColor(ParseTree tree, params object[] paramlist)
	{
		return Blend.SourceColor;
	}

	protected virtual object EvalBlend_InvSrcColor(ParseTree tree, params object[] paramlist)
	{
		return Blend.InverseSourceColor;
	}

	protected virtual object EvalBlend_SrcAlpha(ParseTree tree, params object[] paramlist)
	{
		return Blend.SourceAlpha;
	}

	protected virtual object EvalBlend_InvSrcAlpha(ParseTree tree, params object[] paramlist)
	{
		return Blend.InverseSourceAlpha;
	}

	protected virtual object EvalBlend_DestAlpha(ParseTree tree, params object[] paramlist)
	{
		return Blend.DestinationAlpha;
	}

	protected virtual object EvalBlend_InvDestAlpha(ParseTree tree, params object[] paramlist)
	{
		return Blend.InverseDestinationAlpha;
	}

	protected virtual object EvalBlend_DestColor(ParseTree tree, params object[] paramlist)
	{
		return Blend.DestinationColor;
	}

	protected virtual object EvalBlend_InvDestColor(ParseTree tree, params object[] paramlist)
	{
		return Blend.InverseDestinationColor;
	}

	protected virtual object EvalBlend_SrcAlphaSat(ParseTree tree, params object[] paramlist)
	{
		return Blend.SourceAlphaSaturation;
	}

	protected virtual object EvalBlend_BlendFactor(ParseTree tree, params object[] paramlist)
	{
		return Blend.BlendFactor;
	}

	protected virtual object EvalBlend_InvBlendFactor(ParseTree tree, params object[] paramlist)
	{
		return Blend.InverseBlendFactor;
	}

	protected virtual object EvalBlends(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.Blend_Zero, 0) ?? GetValue(tree, TokenType.Blend_One, 0) ?? GetValue(tree, TokenType.Blend_SrcColor, 0) ?? GetValue(tree, TokenType.Blend_InvSrcColor, 0) ?? GetValue(tree, TokenType.Blend_SrcAlpha, 0) ?? GetValue(tree, TokenType.Blend_InvSrcAlpha, 0) ?? GetValue(tree, TokenType.Blend_DestAlpha, 0) ?? GetValue(tree, TokenType.Blend_InvDestAlpha, 0) ?? GetValue(tree, TokenType.Blend_DestColor, 0) ?? GetValue(tree, TokenType.Blend_InvDestColor, 0) ?? GetValue(tree, TokenType.Blend_SrcAlphaSat, 0) ?? GetValue(tree, TokenType.Blend_BlendFactor, 0) ?? GetValue(tree, TokenType.Blend_InvBlendFactor, 0);
	}

	protected virtual object EvalBlendOp_Add(ParseTree tree, params object[] paramlist)
	{
		return BlendFunction.Add;
	}

	protected virtual object EvalBlendOp_Subtract(ParseTree tree, params object[] paramlist)
	{
		return BlendFunction.Subtract;
	}

	protected virtual object EvalBlendOp_RevSubtract(ParseTree tree, params object[] paramlist)
	{
		return BlendFunction.ReverseSubtract;
	}

	protected virtual object EvalBlendOp_Min(ParseTree tree, params object[] paramlist)
	{
		return BlendFunction.Min;
	}

	protected virtual object EvalBlendOp_Max(ParseTree tree, params object[] paramlist)
	{
		return BlendFunction.Max;
	}

	protected virtual object EvalBlendOps(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.BlendOp_Add, 0) ?? GetValue(tree, TokenType.BlendOp_Subtract, 0) ?? GetValue(tree, TokenType.BlendOp_RevSubtract, 0) ?? GetValue(tree, TokenType.BlendOp_Min, 0) ?? GetValue(tree, TokenType.BlendOp_Max, 0);
	}

	protected virtual object EvalCmpFunc_Never(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.Never;
	}

	protected virtual object EvalCmpFunc_Less(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.Less;
	}

	protected virtual object EvalCmpFunc_Equal(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.Equal;
	}

	protected virtual object EvalCmpFunc_LessEqual(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.LessEqual;
	}

	protected virtual object EvalCmpFunc_Greater(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.Greater;
	}

	protected virtual object EvalCmpFunc_NotEqual(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.NotEqual;
	}

	protected virtual object EvalCmpFunc_GreaterEqual(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.GreaterEqual;
	}

	protected virtual object EvalCmpFunc_Always(ParseTree tree, params object[] paramlist)
	{
		return CompareFunction.Always;
	}

	protected virtual object EvalCmpFunc(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.CmpFunc_Never, 0) ?? GetValue(tree, TokenType.CmpFunc_Less, 0) ?? GetValue(tree, TokenType.CmpFunc_Equal, 0) ?? GetValue(tree, TokenType.CmpFunc_LessEqual, 0) ?? GetValue(tree, TokenType.CmpFunc_Greater, 0) ?? GetValue(tree, TokenType.CmpFunc_NotEqual, 0) ?? GetValue(tree, TokenType.CmpFunc_GreaterEqual, 0) ?? GetValue(tree, TokenType.CmpFunc_Always, 0);
	}

	protected virtual object EvalStencilOp_Keep(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Keep;
	}

	protected virtual object EvalStencilOp_Zero(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Zero;
	}

	protected virtual object EvalStencilOp_Replace(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Replace;
	}

	protected virtual object EvalStencilOp_IncrSat(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.IncrementSaturation;
	}

	protected virtual object EvalStencilOp_DecrSat(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.DecrementSaturation;
	}

	protected virtual object EvalStencilOp_Invert(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Invert;
	}

	protected virtual object EvalStencilOp_Incr(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Increment;
	}

	protected virtual object EvalStencilOp_Decr(ParseTree tree, params object[] paramlist)
	{
		return StencilOperation.Decrement;
	}

	protected virtual object EvalStencilOp(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.StencilOp_Keep, 0) ?? GetValue(tree, TokenType.StencilOp_Zero, 0) ?? GetValue(tree, TokenType.StencilOp_Replace, 0) ?? GetValue(tree, TokenType.StencilOp_IncrSat, 0) ?? GetValue(tree, TokenType.StencilOp_DecrSat, 0) ?? GetValue(tree, TokenType.StencilOp_Invert, 0) ?? GetValue(tree, TokenType.StencilOp_Incr, 0) ?? GetValue(tree, TokenType.StencilOp_Decr, 0);
	}

	protected virtual object EvalRender_State_CullMode(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).CullMode = (CullMode)GetValue(tree, TokenType.CullModes, 0);
		return null;
	}

	protected virtual object EvalRender_State_FillMode(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).FillMode = (FillMode)GetValue(tree, TokenType.FillModes, 0);
		return null;
	}

	protected virtual object EvalRender_State_AlphaBlendEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).AlphaBlendEnable = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_SrcBlend(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).SrcBlend = (Blend)GetValue(tree, TokenType.Blends, 0);
		return null;
	}

	protected virtual object EvalRender_State_DestBlend(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).DestBlend = (Blend)GetValue(tree, TokenType.Blends, 0);
		return null;
	}

	protected virtual object EvalRender_State_BlendOp(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).BlendOp = (BlendFunction)GetValue(tree, TokenType.BlendOps, 0);
		return null;
	}

	protected virtual object EvalRender_State_ColorWriteEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).ColorWriteEnable = (ColorWriteChannels)GetValue(tree, TokenType.ColorsMasks, 0);
		return null;
	}

	protected virtual object EvalRender_State_DepthBias(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).DepthBias = ParseTreeTools.ParseFloat((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalRender_State_SlopeScaleDepthBias(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).SlopeScaleDepthBias = ParseTreeTools.ParseFloat((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalRender_State_ZEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).ZEnable = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_ZWriteEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).ZWriteEnable = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_ZFunc(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).DepthBufferFunction = (CompareFunction)GetValue(tree, TokenType.CmpFunc, 0);
		return null;
	}

	protected virtual object EvalRender_State_MultiSampleAntiAlias(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).MultiSampleAntiAlias = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_ScissorTestEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).ScissorTestEnable = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_StencilEnable(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilEnable = ParseTreeTools.ParseBool((string)GetValue(tree, TokenType.Boolean, 0));
		return null;
	}

	protected virtual object EvalRender_State_StencilFail(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilFail = (StencilOperation)GetValue(tree, TokenType.StencilOp, 0);
		return null;
	}

	protected virtual object EvalRender_State_StencilFunc(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilFunc = (CompareFunction)GetValue(tree, TokenType.CmpFunc, 0);
		return null;
	}

	protected virtual object EvalRender_State_StencilMask(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilMask = ParseTreeTools.ParseInt((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalRender_State_StencilPass(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilPass = (StencilOperation)GetValue(tree, TokenType.StencilOp, 0);
		return null;
	}

	protected virtual object EvalRender_State_StencilRef(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilRef = ParseTreeTools.ParseInt((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalRender_State_StencilWriteMask(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilWriteMask = ParseTreeTools.ParseInt((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalRender_State_StencilZFail(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as PassInfo).StencilZFail = (StencilOperation)GetValue(tree, TokenType.StencilOp, 0);
		return null;
	}

	protected virtual object EvalRender_State_Expression(ParseTree tree, params object[] paramlist)
	{
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, paramlist);
		}
		return null;
	}

	protected virtual object EvalPass_Declaration(ParseTree tree, params object[] paramlist)
	{
		PassInfo pass = new PassInfo();
		pass.name = (GetValue(tree, TokenType.Identifier, 0) as string) ?? string.Empty;
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, pass);
		}
		if (!string.IsNullOrEmpty(pass.psFunction) || !string.IsNullOrEmpty(pass.vsFunction))
		{
			(paramlist[0] as TechniqueInfo).Passes.Add(pass);
		}
		return null;
	}

	protected virtual object EvalVertexShader_Pass_Expression(ParseTree tree, params object[] paramlist)
	{
		PassInfo obj = paramlist[0] as PassInfo;
		obj.vsModel = GetValue(tree, TokenType.ShaderModel, 0) as string;
		obj.vsFunction = GetValue(tree, TokenType.Identifier, 0) as string;
		return null;
	}

	protected virtual object EvalPixelShader_Pass_Expression(ParseTree tree, params object[] paramlist)
	{
		PassInfo obj = paramlist[0] as PassInfo;
		obj.psModel = GetValue(tree, TokenType.ShaderModel, 0) as string;
		obj.psFunction = GetValue(tree, TokenType.Identifier, 0) as string;
		return null;
	}

	protected virtual object EvalAddressMode_Clamp(ParseTree tree, params object[] paramlist)
	{
		return TextureAddressMode.Clamp;
	}

	protected virtual object EvalAddressMode_Wrap(ParseTree tree, params object[] paramlist)
	{
		return TextureAddressMode.Wrap;
	}

	protected virtual object EvalAddressMode_Mirror(ParseTree tree, params object[] paramlist)
	{
		return TextureAddressMode.Mirror;
	}

	protected virtual object EvalAddressMode_Border(ParseTree tree, params object[] paramlist)
	{
		return TextureAddressMode.Border;
	}

	protected virtual object EvalAddressMode(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.AddressMode_Clamp, 0) ?? GetValue(tree, TokenType.AddressMode_Wrap, 0) ?? GetValue(tree, TokenType.AddressMode_Mirror, 0) ?? GetValue(tree, TokenType.AddressMode_Border, 0);
	}

	protected virtual object EvalTextureFilter_None(ParseTree tree, params object[] paramlist)
	{
		return TextureFilterType.None;
	}

	protected virtual object EvalTextureFilter_Linear(ParseTree tree, params object[] paramlist)
	{
		return TextureFilterType.Linear;
	}

	protected virtual object EvalTextureFilter_Point(ParseTree tree, params object[] paramlist)
	{
		return TextureFilterType.Point;
	}

	protected virtual object EvalTextureFilter_Anisotropic(ParseTree tree, params object[] paramlist)
	{
		return TextureFilterType.Anisotropic;
	}

	protected virtual object EvalTextureFilter(ParseTree tree, params object[] paramlist)
	{
		return GetValue(tree, TokenType.TextureFilter_None, 0) ?? GetValue(tree, TokenType.TextureFilter_Linear, 0) ?? GetValue(tree, TokenType.TextureFilter_Point, 0) ?? GetValue(tree, TokenType.TextureFilter_Anisotropic, 0);
	}

	protected virtual object EvalSampler_State_Texture(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).TextureName = (string)GetValue(tree, TokenType.Identifier, 0);
		return null;
	}

	protected virtual object EvalSampler_State_MinFilter(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MinFilter = (TextureFilterType)GetValue(tree, TokenType.TextureFilter, 0);
		return null;
	}

	protected virtual object EvalSampler_State_MagFilter(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MagFilter = (TextureFilterType)GetValue(tree, TokenType.TextureFilter, 0);
		return null;
	}

	protected virtual object EvalSampler_State_MipFilter(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MipFilter = (TextureFilterType)GetValue(tree, TokenType.TextureFilter, 0);
		return null;
	}

	protected virtual object EvalSampler_State_Filter(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).Filter = (TextureFilterType)GetValue(tree, TokenType.TextureFilter, 0);
		return null;
	}

	protected virtual object EvalSampler_State_AddressU(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).AddressU = (TextureAddressMode)GetValue(tree, TokenType.AddressMode, 0);
		return null;
	}

	protected virtual object EvalSampler_State_AddressV(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).AddressV = (TextureAddressMode)GetValue(tree, TokenType.AddressMode, 0);
		return null;
	}

	protected virtual object EvalSampler_State_AddressW(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).AddressW = (TextureAddressMode)GetValue(tree, TokenType.AddressMode, 0);
		return null;
	}

	protected virtual object EvalSampler_State_BorderColor(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).BorderColor = ParseTreeTools.ParseColor((string)GetValue(tree, TokenType.HexColor, 0));
		return null;
	}

	protected virtual object EvalSampler_State_MaxMipLevel(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MaxMipLevel = ParseTreeTools.ParseInt((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalSampler_State_MaxAnisotropy(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MaxAnisotropy = ParseTreeTools.ParseInt((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalSampler_State_MipLodBias(ParseTree tree, params object[] paramlist)
	{
		(paramlist[0] as SamplerStateInfo).MipMapLevelOfDetailBias = ParseTreeTools.ParseFloat((string)GetValue(tree, TokenType.Number, 0));
		return null;
	}

	protected virtual object EvalSampler_State_Expression(ParseTree tree, params object[] paramlist)
	{
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, paramlist);
		}
		return null;
	}

	protected virtual object EvalSampler_Register_Expression(ParseTree tree, params object[] paramlist)
	{
		return null;
	}

	protected virtual object EvalSampler_Declaration_States(ParseTree tree, params object[] paramlist)
	{
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, paramlist);
		}
		return null;
	}

	protected virtual object EvalSampler_Declaration(ParseTree tree, params object[] paramlist)
	{
		if (GetValue(tree, TokenType.Semicolon, 0) == null)
		{
			return null;
		}
		SamplerStateInfo sampler = new SamplerStateInfo();
		sampler.Name = GetValue(tree, TokenType.Identifier, 0) as string;
		foreach (ParseNode node in Nodes)
		{
			node.Eval(tree, sampler);
		}
		(paramlist[0] as ShaderInfo).SamplerStates.Add(sampler.Name, sampler);
		return null;
	}
}

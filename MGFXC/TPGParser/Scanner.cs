using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MGFXC.Effect.TPGParser;

public class Scanner
{
	public string Input;

	public int StartPos;

	public int EndPos;

	public string CurrentFile;

	public int CurrentLine;

	public int CurrentColumn;

	public int CurrentPosition;

	public List<Token> Skipped;

	public Dictionary<TokenType, Regex> Patterns;

	private Token LookAheadToken;

	private List<TokenType> Tokens;

	private List<TokenType> SkipList;

	private readonly TokenType FileAndLine;

	public Scanner()
	{
		Patterns = new Dictionary<TokenType, Regex>();
		Tokens = new List<TokenType>();
		LookAheadToken = null;
		Skipped = new List<Token>();
		SkipList = new List<TokenType>();
		SkipList.Add(TokenType.BlockComment);
		SkipList.Add(TokenType.Comment);
		SkipList.Add(TokenType.Whitespace);
		SkipList.Add(TokenType.LinePragma);
		FileAndLine = TokenType.LinePragma;
		Regex regex = new Regex("/\\*([^*]|\\*[^/])*\\*/", RegexOptions.Compiled);
		Patterns.Add(TokenType.BlockComment, regex);
		Tokens.Add(TokenType.BlockComment);
		regex = new Regex("//[^\\n\\r]*", RegexOptions.Compiled);
		Patterns.Add(TokenType.Comment, regex);
		Tokens.Add(TokenType.Comment);
		regex = new Regex("[ \\t\\n\\r]+", RegexOptions.Compiled);
		Patterns.Add(TokenType.Whitespace, regex);
		Tokens.Add(TokenType.Whitespace);
		regex = new Regex("^[ \\t]*#line[ \\t]*(?<Line>\\d*)[ \\t]*(\\\"(?<File>[^\\\"\\\\]*(?:\\\\.[^\\\"\\\\]*)*)\\\")?\\n", RegexOptions.Compiled);
		Patterns.Add(TokenType.LinePragma, regex);
		Tokens.Add(TokenType.LinePragma);
		regex = new Regex("pass", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Pass, regex);
		Tokens.Add(TokenType.Pass);
		regex = new Regex("technique", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Technique, regex);
		Tokens.Add(TokenType.Technique);
		regex = new Regex("sampler1D|sampler2D|sampler3D|samplerCUBE|SamplerState|sampler", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Sampler, regex);
		Tokens.Add(TokenType.Sampler);
		regex = new Regex("sampler_state", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SamplerState, regex);
		Tokens.Add(TokenType.SamplerState);
		regex = new Regex("VertexShader", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.VertexShader, regex);
		Tokens.Add(TokenType.VertexShader);
		regex = new Regex("PixelShader", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.PixelShader, regex);
		Tokens.Add(TokenType.PixelShader);
		regex = new Regex("register", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Register, regex);
		Tokens.Add(TokenType.Register);
		regex = new Regex("true|false|0|1", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Boolean, regex);
		Tokens.Add(TokenType.Boolean);
		regex = new Regex("[+-]? ?[0-9]?\\.?[0-9]+[fF]?", RegexOptions.Compiled);
		Patterns.Add(TokenType.Number, regex);
		Tokens.Add(TokenType.Number);
		regex = new Regex("0x[0-9a-f]{6}([0-9a-f][0-9a-f])?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.HexColor, regex);
		Tokens.Add(TokenType.HexColor);
		regex = new Regex("[A-Za-z_][A-Za-z0-9_]*", RegexOptions.Compiled);
		Patterns.Add(TokenType.Identifier, regex);
		Tokens.Add(TokenType.Identifier);
		regex = new Regex("{", RegexOptions.Compiled);
		Patterns.Add(TokenType.OpenBracket, regex);
		Tokens.Add(TokenType.OpenBracket);
		regex = new Regex("}", RegexOptions.Compiled);
		Patterns.Add(TokenType.CloseBracket, regex);
		Tokens.Add(TokenType.CloseBracket);
		regex = new Regex("=", RegexOptions.Compiled);
		Patterns.Add(TokenType.Equals, regex);
		Tokens.Add(TokenType.Equals);
		regex = new Regex(":", RegexOptions.Compiled);
		Patterns.Add(TokenType.Colon, regex);
		Tokens.Add(TokenType.Colon);
		regex = new Regex(",", RegexOptions.Compiled);
		Patterns.Add(TokenType.Comma, regex);
		Tokens.Add(TokenType.Comma);
		regex = new Regex(";", RegexOptions.Compiled);
		Patterns.Add(TokenType.Semicolon, regex);
		Tokens.Add(TokenType.Semicolon);
		regex = new Regex("\\|", RegexOptions.Compiled);
		Patterns.Add(TokenType.Or, regex);
		Tokens.Add(TokenType.Or);
		regex = new Regex("\\(", RegexOptions.Compiled);
		Patterns.Add(TokenType.OpenParenthesis, regex);
		Tokens.Add(TokenType.OpenParenthesis);
		regex = new Regex("\\)", RegexOptions.Compiled);
		Patterns.Add(TokenType.CloseParenthesis, regex);
		Tokens.Add(TokenType.CloseParenthesis);
		regex = new Regex("\\[", RegexOptions.Compiled);
		Patterns.Add(TokenType.OpenSquareBracket, regex);
		Tokens.Add(TokenType.OpenSquareBracket);
		regex = new Regex("\\]", RegexOptions.Compiled);
		Patterns.Add(TokenType.CloseSquareBracket, regex);
		Tokens.Add(TokenType.CloseSquareBracket);
		regex = new Regex("<", RegexOptions.Compiled);
		Patterns.Add(TokenType.LessThan, regex);
		Tokens.Add(TokenType.LessThan);
		regex = new Regex(">", RegexOptions.Compiled);
		Patterns.Add(TokenType.GreaterThan, regex);
		Tokens.Add(TokenType.GreaterThan);
		regex = new Regex("compile", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Compile, regex);
		Tokens.Add(TokenType.Compile);
		regex = new Regex("[A-Za-z_][A-Za-z0-9_]*", RegexOptions.Compiled);
		Patterns.Add(TokenType.ShaderModel, regex);
		Tokens.Add(TokenType.ShaderModel);
		regex = new Regex("[\\S]+", RegexOptions.Compiled);
		Patterns.Add(TokenType.Code, regex);
		Tokens.Add(TokenType.Code);
		regex = new Regex("^$", RegexOptions.Compiled);
		Patterns.Add(TokenType.EndOfFile, regex);
		Tokens.Add(TokenType.EndOfFile);
		regex = new Regex("MinFilter", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MinFilter, regex);
		Tokens.Add(TokenType.MinFilter);
		regex = new Regex("MagFilter", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MagFilter, regex);
		Tokens.Add(TokenType.MagFilter);
		regex = new Regex("MipFilter", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MipFilter, regex);
		Tokens.Add(TokenType.MipFilter);
		regex = new Regex("Filter", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Filter, regex);
		Tokens.Add(TokenType.Filter);
		regex = new Regex("Texture", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Texture, regex);
		Tokens.Add(TokenType.Texture);
		regex = new Regex("AddressU", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.AddressU, regex);
		Tokens.Add(TokenType.AddressU);
		regex = new Regex("AddressV", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.AddressV, regex);
		Tokens.Add(TokenType.AddressV);
		regex = new Regex("AddressW", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.AddressW, regex);
		Tokens.Add(TokenType.AddressW);
		regex = new Regex("BorderColor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.BorderColor, regex);
		Tokens.Add(TokenType.BorderColor);
		regex = new Regex("MaxAnisotropy", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MaxAnisotropy, regex);
		Tokens.Add(TokenType.MaxAnisotropy);
		regex = new Regex("MaxMipLevel|MaxLod", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MaxMipLevel, regex);
		Tokens.Add(TokenType.MaxMipLevel);
		regex = new Regex("MipLodBias", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MipLodBias, regex);
		Tokens.Add(TokenType.MipLodBias);
		regex = new Regex("Clamp", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Clamp, regex);
		Tokens.Add(TokenType.Clamp);
		regex = new Regex("Wrap", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Wrap, regex);
		Tokens.Add(TokenType.Wrap);
		regex = new Regex("Mirror", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Mirror, regex);
		Tokens.Add(TokenType.Mirror);
		regex = new Regex("Border", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Border, regex);
		Tokens.Add(TokenType.Border);
		regex = new Regex("None", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.None, regex);
		Tokens.Add(TokenType.None);
		regex = new Regex("Linear", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Linear, regex);
		Tokens.Add(TokenType.Linear);
		regex = new Regex("Point", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Point, regex);
		Tokens.Add(TokenType.Point);
		regex = new Regex("Anisotropic", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Anisotropic, regex);
		Tokens.Add(TokenType.Anisotropic);
		regex = new Regex("AlphaBlendEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.AlphaBlendEnable, regex);
		Tokens.Add(TokenType.AlphaBlendEnable);
		regex = new Regex("SrcBlend", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SrcBlend, regex);
		Tokens.Add(TokenType.SrcBlend);
		regex = new Regex("DestBlend", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.DestBlend, regex);
		Tokens.Add(TokenType.DestBlend);
		regex = new Regex("BlendOp", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.BlendOp, regex);
		Tokens.Add(TokenType.BlendOp);
		regex = new Regex("ColorWriteEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.ColorWriteEnable, regex);
		Tokens.Add(TokenType.ColorWriteEnable);
		regex = new Regex("ZEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.ZEnable, regex);
		Tokens.Add(TokenType.ZEnable);
		regex = new Regex("ZWriteEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.ZWriteEnable, regex);
		Tokens.Add(TokenType.ZWriteEnable);
		regex = new Regex("ZFunc", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.ZFunc, regex);
		Tokens.Add(TokenType.ZFunc);
		regex = new Regex("DepthBias", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.DepthBias, regex);
		Tokens.Add(TokenType.DepthBias);
		regex = new Regex("CullMode", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.CullMode, regex);
		Tokens.Add(TokenType.CullMode);
		regex = new Regex("FillMode", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.FillMode, regex);
		Tokens.Add(TokenType.FillMode);
		regex = new Regex("MultiSampleAntiAlias", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.MultiSampleAntiAlias, regex);
		Tokens.Add(TokenType.MultiSampleAntiAlias);
		regex = new Regex("ScissorTestEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.ScissorTestEnable, regex);
		Tokens.Add(TokenType.ScissorTestEnable);
		regex = new Regex("SlopeScaleDepthBias", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SlopeScaleDepthBias, regex);
		Tokens.Add(TokenType.SlopeScaleDepthBias);
		regex = new Regex("StencilEnable", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilEnable, regex);
		Tokens.Add(TokenType.StencilEnable);
		regex = new Regex("StencilFail", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilFail, regex);
		Tokens.Add(TokenType.StencilFail);
		regex = new Regex("StencilFunc", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilFunc, regex);
		Tokens.Add(TokenType.StencilFunc);
		regex = new Regex("StencilMask", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilMask, regex);
		Tokens.Add(TokenType.StencilMask);
		regex = new Regex("StencilPass", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilPass, regex);
		Tokens.Add(TokenType.StencilPass);
		regex = new Regex("StencilRef", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilRef, regex);
		Tokens.Add(TokenType.StencilRef);
		regex = new Regex("StencilWriteMask", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilWriteMask, regex);
		Tokens.Add(TokenType.StencilWriteMask);
		regex = new Regex("StencilZFail", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.StencilZFail, regex);
		Tokens.Add(TokenType.StencilZFail);
		regex = new Regex("Never", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Never, regex);
		Tokens.Add(TokenType.Never);
		regex = new Regex("Less", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Less, regex);
		Tokens.Add(TokenType.Less);
		regex = new Regex("Equal", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Equal, regex);
		Tokens.Add(TokenType.Equal);
		regex = new Regex("LessEqual", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.LessEqual, regex);
		Tokens.Add(TokenType.LessEqual);
		regex = new Regex("Greater", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Greater, regex);
		Tokens.Add(TokenType.Greater);
		regex = new Regex("NotEqual", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.NotEqual, regex);
		Tokens.Add(TokenType.NotEqual);
		regex = new Regex("GreaterEqual", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.GreaterEqual, regex);
		Tokens.Add(TokenType.GreaterEqual);
		regex = new Regex("Always", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Always, regex);
		Tokens.Add(TokenType.Always);
		regex = new Regex("Keep", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Keep, regex);
		Tokens.Add(TokenType.Keep);
		regex = new Regex("Zero", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Zero, regex);
		Tokens.Add(TokenType.Zero);
		regex = new Regex("Replace", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Replace, regex);
		Tokens.Add(TokenType.Replace);
		regex = new Regex("IncrSat", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.IncrSat, regex);
		Tokens.Add(TokenType.IncrSat);
		regex = new Regex("DecrSat", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.DecrSat, regex);
		Tokens.Add(TokenType.DecrSat);
		regex = new Regex("Invert", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Invert, regex);
		Tokens.Add(TokenType.Invert);
		regex = new Regex("Incr", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Incr, regex);
		Tokens.Add(TokenType.Incr);
		regex = new Regex("Decr", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Decr, regex);
		Tokens.Add(TokenType.Decr);
		regex = new Regex("Red", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Red, regex);
		Tokens.Add(TokenType.Red);
		regex = new Regex("Green", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Green, regex);
		Tokens.Add(TokenType.Green);
		regex = new Regex("Blue", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Blue, regex);
		Tokens.Add(TokenType.Blue);
		regex = new Regex("Alpha", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Alpha, regex);
		Tokens.Add(TokenType.Alpha);
		regex = new Regex("All", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.All, regex);
		Tokens.Add(TokenType.All);
		regex = new Regex("Cw", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Cw, regex);
		Tokens.Add(TokenType.Cw);
		regex = new Regex("Ccw", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Ccw, regex);
		Tokens.Add(TokenType.Ccw);
		regex = new Regex("Solid", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Solid, regex);
		Tokens.Add(TokenType.Solid);
		regex = new Regex("WireFrame", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.WireFrame, regex);
		Tokens.Add(TokenType.WireFrame);
		regex = new Regex("Add", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Add, regex);
		Tokens.Add(TokenType.Add);
		regex = new Regex("Subtract", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Subtract, regex);
		Tokens.Add(TokenType.Subtract);
		regex = new Regex("RevSubtract", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.RevSubtract, regex);
		Tokens.Add(TokenType.RevSubtract);
		regex = new Regex("Min", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Min, regex);
		Tokens.Add(TokenType.Min);
		regex = new Regex("Max", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.Max, regex);
		Tokens.Add(TokenType.Max);
		regex = new Regex("One", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.One, regex);
		Tokens.Add(TokenType.One);
		regex = new Regex("SrcColor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SrcColor, regex);
		Tokens.Add(TokenType.SrcColor);
		regex = new Regex("InvSrcColor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.InvSrcColor, regex);
		Tokens.Add(TokenType.InvSrcColor);
		regex = new Regex("SrcAlpha", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SrcAlpha, regex);
		Tokens.Add(TokenType.SrcAlpha);
		regex = new Regex("InvSrcAlpha", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.InvSrcAlpha, regex);
		Tokens.Add(TokenType.InvSrcAlpha);
		regex = new Regex("DestAlpha", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.DestAlpha, regex);
		Tokens.Add(TokenType.DestAlpha);
		regex = new Regex("InvDestAlpha", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.InvDestAlpha, regex);
		Tokens.Add(TokenType.InvDestAlpha);
		regex = new Regex("DestColor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.DestColor, regex);
		Tokens.Add(TokenType.DestColor);
		regex = new Regex("InvDestColor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.InvDestColor, regex);
		Tokens.Add(TokenType.InvDestColor);
		regex = new Regex("SrcAlphaSat", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.SrcAlphaSat, regex);
		Tokens.Add(TokenType.SrcAlphaSat);
		regex = new Regex("BlendFactor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.BlendFactor, regex);
		Tokens.Add(TokenType.BlendFactor);
		regex = new Regex("InvBlendFactor", RegexOptions.IgnoreCase | RegexOptions.Compiled);
		Patterns.Add(TokenType.InvBlendFactor, regex);
		Tokens.Add(TokenType.InvBlendFactor);
	}

	public void Init(string input)
	{
		Init(input, "");
	}

	public void Init(string input, string fileName)
	{
		Input = input;
		StartPos = 0;
		EndPos = 0;
		CurrentFile = fileName;
		CurrentLine = 1;
		CurrentColumn = 1;
		CurrentPosition = 0;
		LookAheadToken = null;
	}

	public Token GetToken(TokenType type)
	{
		return new Token(StartPos, EndPos)
		{
			Type = type
		};
	}

	public Token Scan(params TokenType[] expectedtokens)
	{
		Token tok = LookAhead(expectedtokens);
		LookAheadToken = null;
		StartPos = tok.EndPos;
		EndPos = tok.EndPos;
		CurrentLine = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
		CurrentFile = tok.File;
		return tok;
	}

	public Token LookAhead(params TokenType[] expectedtokens)
	{
		int startpos = StartPos;
		int endpos = EndPos;
		int currentline = CurrentLine;
		string currentFile = CurrentFile;
		Token tok = null;
		if (LookAheadToken != null && LookAheadToken.Type != TokenType._UNDETERMINED_ && LookAheadToken.Type != 0)
		{
			return LookAheadToken;
		}
		List<TokenType> scantokens;
		if (expectedtokens.Length == 0)
		{
			scantokens = Tokens;
		}
		else
		{
			scantokens = new List<TokenType>(expectedtokens);
			scantokens.AddRange(SkipList);
		}
		do
		{
			int len = -1;
			TokenType index = (TokenType)2147483647;
			string input = Input.Substring(startpos);
			tok = new Token(startpos, endpos);
			for (int i = 0; i < scantokens.Count; i++)
			{
				Match j = Patterns[scantokens[i]].Match(input);
				if (j.Success && j.Index == 0 && (j.Length > len || (scantokens[i] < index && j.Length == len)))
				{
					len = j.Length;
					index = scantokens[i];
				}
			}
			if (index >= TokenType._NONE_ && len >= 0)
			{
				tok.EndPos = startpos + len;
				tok.Text = Input.Substring(tok.StartPos, len);
				tok.Type = index;
			}
			else if (tok.StartPos == tok.EndPos)
			{
				if (tok.StartPos < Input.Length)
				{
					tok.Text = Input.Substring(tok.StartPos, 1);
				}
				else
				{
					tok.Text = "EOF";
				}
			}
			tok.File = currentFile;
			tok.Line = currentline;
			if (tok.StartPos < Input.Length)
			{
				tok.Column = tok.StartPos - Input.LastIndexOf('\n', tok.StartPos);
			}
			if (SkipList.Contains(tok.Type))
			{
				startpos = tok.EndPos;
				endpos = tok.EndPos;
				currentline = tok.Line + (tok.Text.Length - tok.Text.Replace("\n", "").Length);
				currentFile = tok.File;
				Skipped.Add(tok);
			}
			else
			{
				tok.Skipped = Skipped;
				Skipped = new List<Token>();
			}
			if (tok.Type == FileAndLine)
			{
				Match match = Patterns[tok.Type].Match(tok.Text);
				Group fileMatch = match.Groups["File"];
				if (fileMatch.Success)
				{
					currentFile = fileMatch.Value.Replace("\\\\", "\\");
				}
				Group lineMatch = match.Groups["Line"];
				if (lineMatch.Success)
				{
					currentline = int.Parse(lineMatch.Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
				}
			}
		}
		while (SkipList.Contains(tok.Type));
		LookAheadToken = tok;
		return tok;
	}
}

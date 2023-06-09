namespace MGFXC.Effect.TPGParser;

public class Parser
{
	private Scanner scanner;

	private ParseTree tree;

	public Parser(Scanner scanner)
	{
		this.scanner = scanner;
	}

	public ParseTree Parse(string input)
	{
		return Parse(input, "", new ParseTree());
	}

	public ParseTree Parse(string input, string fileName)
	{
		return Parse(input, fileName, new ParseTree());
	}

	public ParseTree Parse(string input, string fileName, ParseTree tree)
	{
		scanner.Init(input, fileName);
		this.tree = tree;
		ParseStart(tree);
		tree.Skipped = scanner.Skipped;
		return tree;
	}

	private void ParseStart(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Start), "Start");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
		ParseNode i;
		while (tok.Type == TokenType.Code || tok.Type == TokenType.Technique || tok.Type == TokenType.Sampler)
		{
			tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
			switch (tok.Type)
			{
			case TokenType.Code:
				tok = scanner.Scan(TokenType.Code);
				i = node.CreateNode(tok, tok.ToString());
				node.Token.UpdateRange(tok);
				node.Nodes.Add(i);
				if (tok.Type != TokenType.Code)
				{
					tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Code, 4097, tok));
					return;
				}
				break;
			case TokenType.Technique:
				ParseTechnique_Declaration(node);
				break;
			case TokenType.Sampler:
				ParseSampler_Declaration(node);
				break;
			default:
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Code, Technique, or Sampler.", 2, tok));
				break;
			}
			tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
		}
		tok = scanner.Scan(TokenType.EndOfFile);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.EndOfFile)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EndOfFile, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseTechnique_Declaration(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Technique_Declaration), "Technique_Declaration");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Technique);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Technique)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Technique, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.Identifier);
		if (tok.Type == TokenType.Identifier)
		{
			tok = scanner.Scan(TokenType.Identifier);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Identifier)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
				return;
			}
		}
		tok = scanner.Scan(TokenType.OpenBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket, 4097, tok));
			return;
		}
		do
		{
			ParsePass_Declaration(node);
			tok = scanner.LookAhead(TokenType.Pass);
		}
		while (tok.Type == TokenType.Pass);
		tok = scanner.Scan(TokenType.CloseBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseFillMode_Solid(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.FillMode_Solid), "FillMode_Solid");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Solid);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Solid)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Solid, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseFillMode_WireFrame(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.FillMode_WireFrame), "FillMode_WireFrame");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.WireFrame);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.WireFrame)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.WireFrame, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseFillModes(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.FillModes), "FillModes");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Solid, TokenType.WireFrame);
		switch (tok.Type)
		{
		case TokenType.Solid:
			ParseFillMode_Solid(node);
			break;
		case TokenType.WireFrame:
			ParseFillMode_WireFrame(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Solid or WireFrame.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseCullMode_None(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CullMode_None), "CullMode_None");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.None);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.None)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.None, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCullMode_Cw(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CullMode_Cw), "CullMode_Cw");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Cw);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Cw)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Cw, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCullMode_Ccw(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CullMode_Ccw), "CullMode_Ccw");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Ccw);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Ccw)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Ccw, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCullModes(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CullModes), "CullModes");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.None, TokenType.Cw, TokenType.Ccw);
		switch (tok.Type)
		{
		case TokenType.None:
			ParseCullMode_None(node);
			break;
		case TokenType.Cw:
			ParseCullMode_Cw(node);
			break;
		case TokenType.Ccw:
			ParseCullMode_Ccw(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected None, Cw, or Ccw.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseColors_None(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_None), "Colors_None");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.None);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.None)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.None, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_Red(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_Red), "Colors_Red");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Red);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Red)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Red, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_Green(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_Green), "Colors_Green");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Green);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Green)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Green, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_Blue(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_Blue), "Colors_Blue");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Blue);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Blue)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Blue, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_Alpha(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_Alpha), "Colors_Alpha");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Alpha);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Alpha)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Alpha, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_All(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_All), "Colors_All");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.All);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.All)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.All, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors_Boolean(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors_Boolean), "Colors_Boolean");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Boolean);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseColors(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Colors), "Colors");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Red, TokenType.Green, TokenType.Blue, TokenType.Alpha, TokenType.None, TokenType.All, TokenType.Boolean);
		switch (tok.Type)
		{
		case TokenType.Red:
			ParseColors_Red(node);
			break;
		case TokenType.Green:
			ParseColors_Green(node);
			break;
		case TokenType.Blue:
			ParseColors_Blue(node);
			break;
		case TokenType.Alpha:
			ParseColors_Alpha(node);
			break;
		case TokenType.None:
			ParseColors_None(node);
			break;
		case TokenType.All:
			ParseColors_All(node);
			break;
		case TokenType.Boolean:
			ParseColors_Boolean(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Red, Green, Blue, Alpha, None, All, or Boolean.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseColorsMasks(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.ColorsMasks), "ColorsMasks");
		parent.Nodes.Add(node);
		ParseColors(node);
		Token tok = scanner.LookAhead(TokenType.Or);
		if (tok.Type == TokenType.Or)
		{
			tok = scanner.Scan(TokenType.Or);
			ParseNode i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Or)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Or, 4097, tok));
				return;
			}
			ParseColors(node);
		}
		tok = scanner.LookAhead(TokenType.Or);
		if (tok.Type == TokenType.Or)
		{
			tok = scanner.Scan(TokenType.Or);
			ParseNode i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Or)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Or, 4097, tok));
				return;
			}
			ParseColors(node);
		}
		tok = scanner.LookAhead(TokenType.Or);
		if (tok.Type == TokenType.Or)
		{
			tok = scanner.Scan(TokenType.Or);
			ParseNode i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Or)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Or, 4097, tok));
				return;
			}
			ParseColors(node);
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseBlend_Zero(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_Zero), "Blend_Zero");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Zero);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Zero)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Zero, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_One(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_One), "Blend_One");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.One);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.One)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.One, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_SrcColor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_SrcColor), "Blend_SrcColor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.SrcColor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.SrcColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SrcColor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_InvSrcColor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_InvSrcColor), "Blend_InvSrcColor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.InvSrcColor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.InvSrcColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.InvSrcColor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_SrcAlpha(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_SrcAlpha), "Blend_SrcAlpha");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.SrcAlpha);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.SrcAlpha)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SrcAlpha, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_InvSrcAlpha(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_InvSrcAlpha), "Blend_InvSrcAlpha");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.InvSrcAlpha);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.InvSrcAlpha)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.InvSrcAlpha, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_DestAlpha(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_DestAlpha), "Blend_DestAlpha");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.DestAlpha);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.DestAlpha)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DestAlpha, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_InvDestAlpha(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_InvDestAlpha), "Blend_InvDestAlpha");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.InvDestAlpha);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.InvDestAlpha)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.InvDestAlpha, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_DestColor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_DestColor), "Blend_DestColor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.DestColor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.DestColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DestColor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_InvDestColor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_InvDestColor), "Blend_InvDestColor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.InvDestColor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.InvDestColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.InvDestColor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_SrcAlphaSat(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_SrcAlphaSat), "Blend_SrcAlphaSat");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.SrcAlphaSat);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.SrcAlphaSat)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SrcAlphaSat, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_BlendFactor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_BlendFactor), "Blend_BlendFactor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.BlendFactor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.BlendFactor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BlendFactor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlend_InvBlendFactor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blend_InvBlendFactor), "Blend_InvBlendFactor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.InvBlendFactor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.InvBlendFactor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.InvBlendFactor, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlends(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Blends), "Blends");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Zero, TokenType.One, TokenType.SrcColor, TokenType.InvSrcColor, TokenType.SrcAlpha, TokenType.InvSrcAlpha, TokenType.DestAlpha, TokenType.InvDestAlpha, TokenType.DestColor, TokenType.InvDestColor, TokenType.SrcAlphaSat, TokenType.BlendFactor, TokenType.InvBlendFactor);
		switch (tok.Type)
		{
		case TokenType.Zero:
			ParseBlend_Zero(node);
			break;
		case TokenType.One:
			ParseBlend_One(node);
			break;
		case TokenType.SrcColor:
			ParseBlend_SrcColor(node);
			break;
		case TokenType.InvSrcColor:
			ParseBlend_InvSrcColor(node);
			break;
		case TokenType.SrcAlpha:
			ParseBlend_SrcAlpha(node);
			break;
		case TokenType.InvSrcAlpha:
			ParseBlend_InvSrcAlpha(node);
			break;
		case TokenType.DestAlpha:
			ParseBlend_DestAlpha(node);
			break;
		case TokenType.InvDestAlpha:
			ParseBlend_InvDestAlpha(node);
			break;
		case TokenType.DestColor:
			ParseBlend_DestColor(node);
			break;
		case TokenType.InvDestColor:
			ParseBlend_InvDestColor(node);
			break;
		case TokenType.SrcAlphaSat:
			ParseBlend_SrcAlphaSat(node);
			break;
		case TokenType.BlendFactor:
			ParseBlend_BlendFactor(node);
			break;
		case TokenType.InvBlendFactor:
			ParseBlend_InvBlendFactor(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Zero, One, SrcColor, InvSrcColor, SrcAlpha, InvSrcAlpha, DestAlpha, InvDestAlpha, DestColor, InvDestColor, SrcAlphaSat, BlendFactor, or InvBlendFactor.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseBlendOp_Add(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOp_Add), "BlendOp_Add");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Add);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Add)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Add, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlendOp_Subtract(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOp_Subtract), "BlendOp_Subtract");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Subtract);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Subtract)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Subtract, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlendOp_RevSubtract(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOp_RevSubtract), "BlendOp_RevSubtract");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.RevSubtract);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.RevSubtract)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.RevSubtract, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlendOp_Min(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOp_Min), "BlendOp_Min");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Min);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Min)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Min, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlendOp_Max(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOp_Max), "BlendOp_Max");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Max);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Max)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Max, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseBlendOps(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.BlendOps), "BlendOps");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Add, TokenType.Subtract, TokenType.RevSubtract, TokenType.Min, TokenType.Max);
		switch (tok.Type)
		{
		case TokenType.Add:
			ParseBlendOp_Add(node);
			break;
		case TokenType.Subtract:
			ParseBlendOp_Subtract(node);
			break;
		case TokenType.RevSubtract:
			ParseBlendOp_RevSubtract(node);
			break;
		case TokenType.Min:
			ParseBlendOp_Min(node);
			break;
		case TokenType.Max:
			ParseBlendOp_Max(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Add, Subtract, RevSubtract, Min, or Max.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseCmpFunc_Never(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_Never), "CmpFunc_Never");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Never);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Never)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Never, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_Less(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_Less), "CmpFunc_Less");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Less);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Less)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Less, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_Equal(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_Equal), "CmpFunc_Equal");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Equal);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equal)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equal, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_LessEqual(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_LessEqual), "CmpFunc_LessEqual");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.LessEqual);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.LessEqual)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.LessEqual, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_Greater(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_Greater), "CmpFunc_Greater");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Greater);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Greater)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Greater, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_NotEqual(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_NotEqual), "CmpFunc_NotEqual");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.NotEqual);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.NotEqual)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.NotEqual, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_GreaterEqual(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_GreaterEqual), "CmpFunc_GreaterEqual");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.GreaterEqual);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.GreaterEqual)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.GreaterEqual, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc_Always(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc_Always), "CmpFunc_Always");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Always);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Always)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Always, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseCmpFunc(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CmpFunc), "CmpFunc");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Never, TokenType.Less, TokenType.Equal, TokenType.LessEqual, TokenType.Greater, TokenType.NotEqual, TokenType.GreaterEqual, TokenType.Always);
		switch (tok.Type)
		{
		case TokenType.Never:
			ParseCmpFunc_Never(node);
			break;
		case TokenType.Less:
			ParseCmpFunc_Less(node);
			break;
		case TokenType.Equal:
			ParseCmpFunc_Equal(node);
			break;
		case TokenType.LessEqual:
			ParseCmpFunc_LessEqual(node);
			break;
		case TokenType.Greater:
			ParseCmpFunc_Greater(node);
			break;
		case TokenType.NotEqual:
			ParseCmpFunc_NotEqual(node);
			break;
		case TokenType.GreaterEqual:
			ParseCmpFunc_GreaterEqual(node);
			break;
		case TokenType.Always:
			ParseCmpFunc_Always(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Never, Less, Equal, LessEqual, Greater, NotEqual, GreaterEqual, or Always.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseStencilOp_Keep(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Keep), "StencilOp_Keep");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Keep);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Keep)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Keep, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_Zero(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Zero), "StencilOp_Zero");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Zero);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Zero)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Zero, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_Replace(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Replace), "StencilOp_Replace");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Replace);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Replace)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Replace, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_IncrSat(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_IncrSat), "StencilOp_IncrSat");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.IncrSat);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.IncrSat)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IncrSat, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_DecrSat(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_DecrSat), "StencilOp_DecrSat");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.DecrSat);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.DecrSat)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DecrSat, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_Invert(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Invert), "StencilOp_Invert");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Invert);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Invert)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Invert, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_Incr(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Incr), "StencilOp_Incr");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Incr);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Incr)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Incr, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp_Decr(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp_Decr), "StencilOp_Decr");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Decr);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Decr)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Decr, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseStencilOp(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.StencilOp), "StencilOp");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Keep, TokenType.Zero, TokenType.Replace, TokenType.IncrSat, TokenType.DecrSat, TokenType.Invert, TokenType.Incr, TokenType.Decr);
		switch (tok.Type)
		{
		case TokenType.Keep:
			ParseStencilOp_Keep(node);
			break;
		case TokenType.Zero:
			ParseStencilOp_Zero(node);
			break;
		case TokenType.Replace:
			ParseStencilOp_Replace(node);
			break;
		case TokenType.IncrSat:
			ParseStencilOp_IncrSat(node);
			break;
		case TokenType.DecrSat:
			ParseStencilOp_DecrSat(node);
			break;
		case TokenType.Invert:
			ParseStencilOp_Invert(node);
			break;
		case TokenType.Incr:
			ParseStencilOp_Incr(node);
			break;
		case TokenType.Decr:
			ParseStencilOp_Decr(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Keep, Zero, Replace, IncrSat, DecrSat, Invert, Incr, or Decr.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseRender_State_CullMode(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_CullMode), "Render_State_CullMode");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.CullMode);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CullMode)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CullMode, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseCullModes(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_FillMode(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_FillMode), "Render_State_FillMode");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.FillMode);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.FillMode)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.FillMode, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseFillModes(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_AlphaBlendEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_AlphaBlendEnable), "Render_State_AlphaBlendEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.AlphaBlendEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.AlphaBlendEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.AlphaBlendEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_SrcBlend(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_SrcBlend), "Render_State_SrcBlend");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.SrcBlend);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.SrcBlend)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SrcBlend, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseBlends(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_DestBlend(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_DestBlend), "Render_State_DestBlend");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.DestBlend);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.DestBlend)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DestBlend, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseBlends(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_BlendOp(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_BlendOp), "Render_State_BlendOp");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.BlendOp);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.BlendOp)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BlendOp, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseBlendOps(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_ColorWriteEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_ColorWriteEnable), "Render_State_ColorWriteEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.ColorWriteEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ColorWriteEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ColorWriteEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseColorsMasks(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_DepthBias(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_DepthBias), "Render_State_DepthBias");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.DepthBias);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.DepthBias)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.DepthBias, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_SlopeScaleDepthBias(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_SlopeScaleDepthBias), "Render_State_SlopeScaleDepthBias");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.SlopeScaleDepthBias);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.SlopeScaleDepthBias)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SlopeScaleDepthBias, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_ZEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_ZEnable), "Render_State_ZEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.ZEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ZEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ZEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_ZWriteEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_ZWriteEnable), "Render_State_ZWriteEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.ZWriteEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ZWriteEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ZWriteEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_ZFunc(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_ZFunc), "Render_State_ZFunc");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.ZFunc);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ZFunc)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ZFunc, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseCmpFunc(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_MultiSampleAntiAlias(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_MultiSampleAntiAlias), "Render_State_MultiSampleAntiAlias");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MultiSampleAntiAlias);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MultiSampleAntiAlias)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MultiSampleAntiAlias, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_ScissorTestEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_ScissorTestEnable), "Render_State_ScissorTestEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.ScissorTestEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ScissorTestEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ScissorTestEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilEnable(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilEnable), "Render_State_StencilEnable");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilEnable);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilEnable)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilEnable, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Boolean);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Boolean)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Boolean, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilFail(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilFail), "Render_State_StencilFail");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilFail);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilFail)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilFail, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseStencilOp(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilFunc(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilFunc), "Render_State_StencilFunc");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilFunc);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilFunc)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilFunc, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseCmpFunc(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilMask(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilMask), "Render_State_StencilMask");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilMask);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilMask)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilMask, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilPass(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilPass), "Render_State_StencilPass");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilPass);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilPass)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilPass, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseStencilOp(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilRef(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilRef), "Render_State_StencilRef");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilRef);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilRef)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilRef, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilWriteMask(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilWriteMask), "Render_State_StencilWriteMask");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilWriteMask);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilWriteMask)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilWriteMask, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_StencilZFail(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_StencilZFail), "Render_State_StencilZFail");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.StencilZFail);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.StencilZFail)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.StencilZFail, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseStencilOp(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseRender_State_Expression(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_Expression), "Render_State_Expression");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.CullMode, TokenType.FillMode, TokenType.AlphaBlendEnable, TokenType.SrcBlend, TokenType.DestBlend, TokenType.BlendOp, TokenType.ColorWriteEnable, TokenType.DepthBias, TokenType.SlopeScaleDepthBias, TokenType.ZEnable, TokenType.ZWriteEnable, TokenType.ZFunc, TokenType.MultiSampleAntiAlias, TokenType.ScissorTestEnable, TokenType.StencilEnable, TokenType.StencilFail, TokenType.StencilFunc, TokenType.StencilMask, TokenType.StencilPass, TokenType.StencilRef, TokenType.StencilWriteMask, TokenType.StencilZFail);
		switch (tok.Type)
		{
		case TokenType.CullMode:
			ParseRender_State_CullMode(node);
			break;
		case TokenType.FillMode:
			ParseRender_State_FillMode(node);
			break;
		case TokenType.AlphaBlendEnable:
			ParseRender_State_AlphaBlendEnable(node);
			break;
		case TokenType.SrcBlend:
			ParseRender_State_SrcBlend(node);
			break;
		case TokenType.DestBlend:
			ParseRender_State_DestBlend(node);
			break;
		case TokenType.BlendOp:
			ParseRender_State_BlendOp(node);
			break;
		case TokenType.ColorWriteEnable:
			ParseRender_State_ColorWriteEnable(node);
			break;
		case TokenType.DepthBias:
			ParseRender_State_DepthBias(node);
			break;
		case TokenType.SlopeScaleDepthBias:
			ParseRender_State_SlopeScaleDepthBias(node);
			break;
		case TokenType.ZEnable:
			ParseRender_State_ZEnable(node);
			break;
		case TokenType.ZWriteEnable:
			ParseRender_State_ZWriteEnable(node);
			break;
		case TokenType.ZFunc:
			ParseRender_State_ZFunc(node);
			break;
		case TokenType.MultiSampleAntiAlias:
			ParseRender_State_MultiSampleAntiAlias(node);
			break;
		case TokenType.ScissorTestEnable:
			ParseRender_State_ScissorTestEnable(node);
			break;
		case TokenType.StencilEnable:
			ParseRender_State_StencilEnable(node);
			break;
		case TokenType.StencilFail:
			ParseRender_State_StencilFail(node);
			break;
		case TokenType.StencilFunc:
			ParseRender_State_StencilFunc(node);
			break;
		case TokenType.StencilMask:
			ParseRender_State_StencilMask(node);
			break;
		case TokenType.StencilPass:
			ParseRender_State_StencilPass(node);
			break;
		case TokenType.StencilRef:
			ParseRender_State_StencilRef(node);
			break;
		case TokenType.StencilWriteMask:
			ParseRender_State_StencilWriteMask(node);
			break;
		case TokenType.StencilZFail:
			ParseRender_State_StencilZFail(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected CullMode, FillMode, AlphaBlendEnable, SrcBlend, DestBlend, BlendOp, ColorWriteEnable, DepthBias, SlopeScaleDepthBias, ZEnable, ZWriteEnable, ZFunc, MultiSampleAntiAlias, ScissorTestEnable, StencilEnable, StencilFail, StencilFunc, StencilMask, StencilPass, StencilRef, StencilWriteMask, or StencilZFail.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParsePass_Declaration(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Pass_Declaration), "Pass_Declaration");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Pass);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Pass)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Pass, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.Identifier);
		if (tok.Type == TokenType.Identifier)
		{
			tok = scanner.Scan(TokenType.Identifier);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Identifier)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
				return;
			}
		}
		tok = scanner.Scan(TokenType.OpenBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.CullMode, TokenType.FillMode, TokenType.AlphaBlendEnable, TokenType.SrcBlend, TokenType.DestBlend, TokenType.BlendOp, TokenType.ColorWriteEnable, TokenType.DepthBias, TokenType.SlopeScaleDepthBias, TokenType.ZEnable, TokenType.ZWriteEnable, TokenType.ZFunc, TokenType.MultiSampleAntiAlias, TokenType.ScissorTestEnable, TokenType.StencilEnable, TokenType.StencilFail, TokenType.StencilFunc, TokenType.StencilMask, TokenType.StencilPass, TokenType.StencilRef, TokenType.StencilWriteMask, TokenType.StencilZFail);
		while (tok.Type == TokenType.VertexShader || tok.Type == TokenType.PixelShader || tok.Type == TokenType.CullMode || tok.Type == TokenType.FillMode || tok.Type == TokenType.AlphaBlendEnable || tok.Type == TokenType.SrcBlend || tok.Type == TokenType.DestBlend || tok.Type == TokenType.BlendOp || tok.Type == TokenType.ColorWriteEnable || tok.Type == TokenType.DepthBias || tok.Type == TokenType.SlopeScaleDepthBias || tok.Type == TokenType.ZEnable || tok.Type == TokenType.ZWriteEnable || tok.Type == TokenType.ZFunc || tok.Type == TokenType.MultiSampleAntiAlias || tok.Type == TokenType.ScissorTestEnable || tok.Type == TokenType.StencilEnable || tok.Type == TokenType.StencilFail || tok.Type == TokenType.StencilFunc || tok.Type == TokenType.StencilMask || tok.Type == TokenType.StencilPass || tok.Type == TokenType.StencilRef || tok.Type == TokenType.StencilWriteMask || tok.Type == TokenType.StencilZFail)
		{
			tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.CullMode, TokenType.FillMode, TokenType.AlphaBlendEnable, TokenType.SrcBlend, TokenType.DestBlend, TokenType.BlendOp, TokenType.ColorWriteEnable, TokenType.DepthBias, TokenType.SlopeScaleDepthBias, TokenType.ZEnable, TokenType.ZWriteEnable, TokenType.ZFunc, TokenType.MultiSampleAntiAlias, TokenType.ScissorTestEnable, TokenType.StencilEnable, TokenType.StencilFail, TokenType.StencilFunc, TokenType.StencilMask, TokenType.StencilPass, TokenType.StencilRef, TokenType.StencilWriteMask, TokenType.StencilZFail);
			switch (tok.Type)
			{
			case TokenType.VertexShader:
				ParseVertexShader_Pass_Expression(node);
				break;
			case TokenType.PixelShader:
				ParsePixelShader_Pass_Expression(node);
				break;
			case TokenType.AlphaBlendEnable:
			case TokenType.SrcBlend:
			case TokenType.DestBlend:
			case TokenType.BlendOp:
			case TokenType.ColorWriteEnable:
			case TokenType.ZEnable:
			case TokenType.ZWriteEnable:
			case TokenType.ZFunc:
			case TokenType.DepthBias:
			case TokenType.CullMode:
			case TokenType.FillMode:
			case TokenType.MultiSampleAntiAlias:
			case TokenType.ScissorTestEnable:
			case TokenType.SlopeScaleDepthBias:
			case TokenType.StencilEnable:
			case TokenType.StencilFail:
			case TokenType.StencilFunc:
			case TokenType.StencilMask:
			case TokenType.StencilPass:
			case TokenType.StencilRef:
			case TokenType.StencilWriteMask:
			case TokenType.StencilZFail:
				ParseRender_State_Expression(node);
				break;
			default:
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected VertexShader, PixelShader, CullMode, FillMode, AlphaBlendEnable, SrcBlend, DestBlend, BlendOp, ColorWriteEnable, DepthBias, SlopeScaleDepthBias, ZEnable, ZWriteEnable, ZFunc, MultiSampleAntiAlias, ScissorTestEnable, StencilEnable, StencilFail, StencilFunc, StencilMask, StencilPass, StencilRef, StencilWriteMask, or StencilZFail.", 2, tok));
				break;
			}
			tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.CullMode, TokenType.FillMode, TokenType.AlphaBlendEnable, TokenType.SrcBlend, TokenType.DestBlend, TokenType.BlendOp, TokenType.ColorWriteEnable, TokenType.DepthBias, TokenType.SlopeScaleDepthBias, TokenType.ZEnable, TokenType.ZWriteEnable, TokenType.ZFunc, TokenType.MultiSampleAntiAlias, TokenType.ScissorTestEnable, TokenType.StencilEnable, TokenType.StencilFail, TokenType.StencilFunc, TokenType.StencilMask, TokenType.StencilPass, TokenType.StencilRef, TokenType.StencilWriteMask, TokenType.StencilZFail);
		}
		tok = scanner.Scan(TokenType.CloseBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseVertexShader_Pass_Expression(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.VertexShader_Pass_Expression), "VertexShader_Pass_Expression");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.VertexShader);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.VertexShader)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.VertexShader, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Compile);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Compile)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Compile, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.ShaderModel);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ShaderModel)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ShaderModel, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Identifier);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Identifier)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.OpenParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.CloseParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParsePixelShader_Pass_Expression(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.PixelShader_Pass_Expression), "PixelShader_Pass_Expression");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.PixelShader);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.PixelShader)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PixelShader, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Compile);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Compile)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Compile, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.ShaderModel);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.ShaderModel)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ShaderModel, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Identifier);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Identifier)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.OpenParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.CloseParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseAddressMode_Clamp(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddressMode_Clamp), "AddressMode_Clamp");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Clamp);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Clamp)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Clamp, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseAddressMode_Wrap(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddressMode_Wrap), "AddressMode_Wrap");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Wrap);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Wrap)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Wrap, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseAddressMode_Mirror(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddressMode_Mirror), "AddressMode_Mirror");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Mirror);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Mirror)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Mirror, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseAddressMode_Border(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddressMode_Border), "AddressMode_Border");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Border);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Border)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Border, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseAddressMode(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.AddressMode), "AddressMode");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Clamp, TokenType.Wrap, TokenType.Mirror, TokenType.Border);
		switch (tok.Type)
		{
		case TokenType.Clamp:
			ParseAddressMode_Clamp(node);
			break;
		case TokenType.Wrap:
			ParseAddressMode_Wrap(node);
			break;
		case TokenType.Mirror:
			ParseAddressMode_Mirror(node);
			break;
		case TokenType.Border:
			ParseAddressMode_Border(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Clamp, Wrap, Mirror, or Border.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseTextureFilter_None(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.TextureFilter_None), "TextureFilter_None");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.None);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.None)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.None, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseTextureFilter_Linear(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.TextureFilter_Linear), "TextureFilter_Linear");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Linear);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Linear)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Linear, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseTextureFilter_Point(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.TextureFilter_Point), "TextureFilter_Point");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Point);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Point)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Point, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseTextureFilter_Anisotropic(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.TextureFilter_Anisotropic), "TextureFilter_Anisotropic");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Anisotropic);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Anisotropic)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Anisotropic, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseTextureFilter(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.TextureFilter), "TextureFilter");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.None, TokenType.Linear, TokenType.Point, TokenType.Anisotropic);
		switch (tok.Type)
		{
		case TokenType.None:
			ParseTextureFilter_None(node);
			break;
		case TokenType.Linear:
			ParseTextureFilter_Linear(node);
			break;
		case TokenType.Point:
			ParseTextureFilter_Point(node);
			break;
		case TokenType.Anisotropic:
			ParseTextureFilter_Anisotropic(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected None, Linear, Point, or Anisotropic.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseSampler_State_Texture(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_Texture), "Sampler_State_Texture");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Texture);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Texture)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Texture, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.LessThan, TokenType.OpenParenthesis);
		switch (tok.Type)
		{
		case TokenType.LessThan:
			tok = scanner.Scan(TokenType.LessThan);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.LessThan)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.LessThan, 4097, tok));
				return;
			}
			break;
		case TokenType.OpenParenthesis:
			tok = scanner.Scan(TokenType.OpenParenthesis);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.OpenParenthesis)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis, 4097, tok));
				return;
			}
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected LessThan or OpenParenthesis.", 2, tok));
			break;
		}
		tok = scanner.Scan(TokenType.Identifier);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Identifier)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.GreaterThan, TokenType.CloseParenthesis);
		switch (tok.Type)
		{
		case TokenType.GreaterThan:
			tok = scanner.Scan(TokenType.GreaterThan);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.GreaterThan)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.GreaterThan, 4097, tok));
				return;
			}
			break;
		case TokenType.CloseParenthesis:
			tok = scanner.Scan(TokenType.CloseParenthesis);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.CloseParenthesis)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis, 4097, tok));
				return;
			}
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected GreaterThan or CloseParenthesis.", 2, tok));
			break;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MinFilter(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MinFilter), "Sampler_State_MinFilter");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MinFilter);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MinFilter)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MinFilter, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseTextureFilter(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MagFilter(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MagFilter), "Sampler_State_MagFilter");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MagFilter);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MagFilter)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MagFilter, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseTextureFilter(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MipFilter(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MipFilter), "Sampler_State_MipFilter");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MipFilter);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MipFilter)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MipFilter, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseTextureFilter(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_Filter(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_Filter), "Sampler_State_Filter");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Filter);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Filter)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Filter, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseTextureFilter(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_AddressU(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_AddressU), "Sampler_State_AddressU");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.AddressU);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.AddressU)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.AddressU, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseAddressMode(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_AddressV(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_AddressV), "Sampler_State_AddressV");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.AddressV);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.AddressV)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.AddressV, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseAddressMode(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_AddressW(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_AddressW), "Sampler_State_AddressW");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.AddressW);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.AddressW)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.AddressW, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		ParseAddressMode(node);
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_BorderColor(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_BorderColor), "Sampler_State_BorderColor");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.BorderColor);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.BorderColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BorderColor, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.HexColor);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.HexColor)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.HexColor, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MaxMipLevel(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MaxMipLevel), "Sampler_State_MaxMipLevel");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MaxMipLevel);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MaxMipLevel)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MaxMipLevel, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MaxAnisotropy(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MaxAnisotropy), "Sampler_State_MaxAnisotropy");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MaxAnisotropy);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MaxAnisotropy)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MaxAnisotropy, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_MipLodBias(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_MipLodBias), "Sampler_State_MipLodBias");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.MipLodBias);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.MipLodBias)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.MipLodBias, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Equals);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Equals)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Number);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Number)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Semicolon);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Semicolon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_State_Expression(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_Expression), "Sampler_State_Expression");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Texture, TokenType.MinFilter, TokenType.MagFilter, TokenType.MipFilter, TokenType.Filter, TokenType.AddressU, TokenType.AddressV, TokenType.AddressW, TokenType.BorderColor, TokenType.MaxMipLevel, TokenType.MaxAnisotropy, TokenType.MipLodBias);
		switch (tok.Type)
		{
		case TokenType.Texture:
			ParseSampler_State_Texture(node);
			break;
		case TokenType.MinFilter:
			ParseSampler_State_MinFilter(node);
			break;
		case TokenType.MagFilter:
			ParseSampler_State_MagFilter(node);
			break;
		case TokenType.MipFilter:
			ParseSampler_State_MipFilter(node);
			break;
		case TokenType.Filter:
			ParseSampler_State_Filter(node);
			break;
		case TokenType.AddressU:
			ParseSampler_State_AddressU(node);
			break;
		case TokenType.AddressV:
			ParseSampler_State_AddressV(node);
			break;
		case TokenType.AddressW:
			ParseSampler_State_AddressW(node);
			break;
		case TokenType.BorderColor:
			ParseSampler_State_BorderColor(node);
			break;
		case TokenType.MaxMipLevel:
			ParseSampler_State_MaxMipLevel(node);
			break;
		case TokenType.MaxAnisotropy:
			ParseSampler_State_MaxAnisotropy(node);
			break;
		case TokenType.MipLodBias:
			ParseSampler_State_MipLodBias(node);
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Texture, MinFilter, MagFilter, MipFilter, Filter, AddressU, AddressV, AddressW, BorderColor, MaxMipLevel, MaxAnisotropy, or MipLodBias.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}

	private void ParseSampler_Register_Expression(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_Register_Expression), "Sampler_Register_Expression");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Colon);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Colon)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Colon, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Register);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Register)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Register, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.OpenParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Identifier);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Identifier)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.Comma);
		if (tok.Type == TokenType.Comma)
		{
			tok = scanner.Scan(TokenType.Comma);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Comma)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Comma, 4097, tok));
				return;
			}
			tok = scanner.Scan(TokenType.Identifier);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Identifier)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
				return;
			}
			tok = scanner.LookAhead(TokenType.OpenSquareBracket);
			if (tok.Type == TokenType.OpenSquareBracket)
			{
				tok = scanner.Scan(TokenType.OpenSquareBracket);
				i = node.CreateNode(tok, tok.ToString());
				node.Token.UpdateRange(tok);
				node.Nodes.Add(i);
				if (tok.Type != TokenType.OpenSquareBracket)
				{
					tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenSquareBracket, 4097, tok));
					return;
				}
				tok = scanner.Scan(TokenType.Number);
				i = node.CreateNode(tok, tok.ToString());
				node.Token.UpdateRange(tok);
				node.Nodes.Add(i);
				if (tok.Type != TokenType.Number)
				{
					tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number, 4097, tok));
					return;
				}
				tok = scanner.Scan(TokenType.CloseSquareBracket);
				i = node.CreateNode(tok, tok.ToString());
				node.Token.UpdateRange(tok);
				node.Nodes.Add(i);
				if (tok.Type != TokenType.CloseSquareBracket)
				{
					tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseSquareBracket, 4097, tok));
					return;
				}
			}
		}
		tok = scanner.Scan(TokenType.CloseParenthesis);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseParenthesis)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_Declaration_States(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_Declaration_States), "Sampler_Declaration_States");
		parent.Nodes.Add(node);
		Token tok = scanner.LookAhead(TokenType.Equals);
		ParseNode i;
		if (tok.Type == TokenType.Equals)
		{
			tok = scanner.Scan(TokenType.Equals);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Equals)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals, 4097, tok));
				return;
			}
			tok = scanner.Scan(TokenType.SamplerState);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.SamplerState)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SamplerState, 4097, tok));
				return;
			}
		}
		tok = scanner.Scan(TokenType.OpenBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.OpenBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.Texture, TokenType.MinFilter, TokenType.MagFilter, TokenType.MipFilter, TokenType.Filter, TokenType.AddressU, TokenType.AddressV, TokenType.AddressW, TokenType.BorderColor, TokenType.MaxMipLevel, TokenType.MaxAnisotropy, TokenType.MipLodBias);
		while (tok.Type == TokenType.Texture || tok.Type == TokenType.MinFilter || tok.Type == TokenType.MagFilter || tok.Type == TokenType.MipFilter || tok.Type == TokenType.Filter || tok.Type == TokenType.AddressU || tok.Type == TokenType.AddressV || tok.Type == TokenType.AddressW || tok.Type == TokenType.BorderColor || tok.Type == TokenType.MaxMipLevel || tok.Type == TokenType.MaxAnisotropy || tok.Type == TokenType.MipLodBias)
		{
			ParseSampler_State_Expression(node);
			tok = scanner.LookAhead(TokenType.Texture, TokenType.MinFilter, TokenType.MagFilter, TokenType.MipFilter, TokenType.Filter, TokenType.AddressU, TokenType.AddressV, TokenType.AddressW, TokenType.BorderColor, TokenType.MaxMipLevel, TokenType.MaxAnisotropy, TokenType.MipLodBias);
		}
		tok = scanner.Scan(TokenType.CloseBracket);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.CloseBracket)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket, 4097, tok));
		}
		else
		{
			parent.Token.UpdateRange(node.Token);
		}
	}

	private void ParseSampler_Declaration(ParseNode parent)
	{
		ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_Declaration), "Sampler_Declaration");
		parent.Nodes.Add(node);
		Token tok = scanner.Scan(TokenType.Sampler);
		ParseNode i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Sampler)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Sampler, 4097, tok));
			return;
		}
		tok = scanner.Scan(TokenType.Identifier);
		i = node.CreateNode(tok, tok.ToString());
		node.Token.UpdateRange(tok);
		node.Nodes.Add(i);
		if (tok.Type != TokenType.Identifier)
		{
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier, 4097, tok));
			return;
		}
		tok = scanner.LookAhead(TokenType.Colon);
		while (tok.Type == TokenType.Colon)
		{
			ParseSampler_Register_Expression(node);
			tok = scanner.LookAhead(TokenType.Colon);
		}
		tok = scanner.LookAhead(TokenType.Equals, TokenType.OpenBracket);
		if (tok.Type == TokenType.Equals || tok.Type == TokenType.OpenBracket)
		{
			ParseSampler_Declaration_States(node);
		}
		tok = scanner.LookAhead(TokenType.Semicolon, TokenType.Comma, TokenType.CloseParenthesis);
		switch (tok.Type)
		{
		case TokenType.Semicolon:
			tok = scanner.Scan(TokenType.Semicolon);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Semicolon)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon, 4097, tok));
				return;
			}
			break;
		case TokenType.Comma:
			tok = scanner.Scan(TokenType.Comma);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.Comma)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Comma, 4097, tok));
				return;
			}
			break;
		case TokenType.CloseParenthesis:
			tok = scanner.Scan(TokenType.CloseParenthesis);
			i = node.CreateNode(tok, tok.ToString());
			node.Token.UpdateRange(tok);
			node.Nodes.Add(i);
			if (tok.Type != TokenType.CloseParenthesis)
			{
				tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis, 4097, tok));
				return;
			}
			break;
		default:
			tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected Semicolon, Comma, or CloseParenthesis.", 2, tok));
			break;
		}
		parent.Token.UpdateRange(node.Token);
	}
}

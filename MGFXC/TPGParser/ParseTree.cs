using System;
using System.Collections.Generic;
using System.Text;

namespace MGFXC.Effect.TPGParser;

[Serializable]
public class ParseTree : ParseNode
{
	public ParseErrors Errors;

	public List<Token> Skipped;

	public ParseTree()
		: base(new Token(), "ParseTree")
	{
		Token.Type = TokenType.Start;
		Token.Text = "Root";
		Errors = new ParseErrors();
	}

	public string PrintTree()
	{
		StringBuilder sb = new StringBuilder();
		int indent = 0;
		PrintNode(sb, this, indent);
		return sb.ToString();
	}

	private void PrintNode(StringBuilder sb, ParseNode node, int indent)
	{
		string space = "".PadLeft(indent, ' ');
		sb.Append(space);
		sb.AppendLine(node.Text);
		foreach (ParseNode i in node.Nodes)
		{
			PrintNode(sb, i, indent + 2);
		}
	}

	public object Eval(params object[] paramlist)
	{
		return base.Nodes[0].Eval(this, paramlist);
	}
}

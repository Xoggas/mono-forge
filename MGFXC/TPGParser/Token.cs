using System.Collections.Generic;
using System.Xml.Serialization;

namespace MGFXC.Effect.TPGParser;

public class Token
{
	private string file;

	private int line;

	private int column;

	private int startpos;

	private int endpos;

	private string text;

	private object value;

	private List<Token> skipped;

	[XmlAttribute]
	public TokenType Type;

	public string File
	{
		get
		{
			return file;
		}
		set
		{
			file = value;
		}
	}

	public int Line
	{
		get
		{
			return line;
		}
		set
		{
			line = value;
		}
	}

	public int Column
	{
		get
		{
			return column;
		}
		set
		{
			column = value;
		}
	}

	public int StartPos
	{
		get
		{
			return startpos;
		}
		set
		{
			startpos = value;
		}
	}

	public int Length => endpos - startpos;

	public int EndPos
	{
		get
		{
			return endpos;
		}
		set
		{
			endpos = value;
		}
	}

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

	public List<Token> Skipped
	{
		get
		{
			return skipped;
		}
		set
		{
			skipped = value;
		}
	}

	public object Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public Token()
		: this(0, 0)
	{
	}

	public Token(int start, int end)
	{
		Type = TokenType._UNDETERMINED_;
		startpos = start;
		endpos = end;
		Text = "";
		Value = null;
	}

	public void UpdateRange(Token token)
	{
		if (token.StartPos < startpos)
		{
			startpos = token.StartPos;
		}
		if (token.EndPos > endpos)
		{
			endpos = token.EndPos;
		}
	}

	public override string ToString()
	{
		if (Text != null)
		{
			return Type.ToString() + " '" + Text + "'";
		}
		return Type.ToString();
	}
}

using System;

namespace MGFXC.Effect.TPGParser;

[Serializable]
public class ParseError
{
	private string file;

	private string message;

	private int code;

	private int line;

	private int col;

	private int pos;

	private int length;

	public string File => file;

	public int Code => code;

	public int Line => line;

	public int Column => col;

	public int Position => pos;

	public int Length => length;

	public string Message => message;

	public ParseError()
	{
	}

	public ParseError(string message, int code, ParseNode node)
		: this(message, code, node.Token)
	{
	}

	public ParseError(string message, int code, Token token)
		: this(message, code, token.File, token.Line, token.Column, token.StartPos, token.Length)
	{
	}

	public ParseError(string message, int code)
		: this(message, code, string.Empty, 0, 0, 0, 0)
	{
	}

	public ParseError(string message, int code, string file, int line, int col, int pos, int length)
	{
		this.file = file;
		this.message = message;
		this.code = code;
		this.line = line;
		this.col = col;
		this.pos = pos;
		this.length = length;
	}
}

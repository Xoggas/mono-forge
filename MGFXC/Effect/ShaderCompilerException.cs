using System;

namespace MGFXC.Effect;

public class ShaderCompilerException : Exception
{
	public ShaderCompilerException()
		: base("A shader failed to compile!")
	{
	}
}

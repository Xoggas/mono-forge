namespace MGFXC.Effect;

public class Options
{
	[CommandLineParser.Required]
	public string SourceFile;

	[CommandLineParser.Required]
	public string OutputFile = string.Empty;

	[CommandLineParser.ProfileName]
	public ShaderProfile Profile = ShaderProfile.OpenGL;

	[CommandLineParser.Name("Debug", "\t\t - Include extra debug information in the compiled effect.")]
	public bool Debug;

	[CommandLineParser.Name("Defines", "\t - Semicolon-delimited define assignments")]
	public string Defines;
}

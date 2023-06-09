using System;
using System.Collections.Generic;
using System.IO;
using MGFXC.Effect.TPGParser;

namespace MGFXC.Effect;

public class ShaderResult
{
	public ShaderInfo ShaderInfo { get; private set; }

	public string FilePath { get; private set; }

	public string FileContent { get; private set; }

	public string OutputFilePath { get; private set; }

	public List<string> Dependencies { get; private set; }

	public List<string> AdditionalOutputFiles { get; private set; }

	public ShaderProfile Profile { get; private set; }

	public bool Debug { get; private set; }

	public static ShaderResult FromFile(string path, Options options, IEffectCompilerOutput output)
	{
		return FromString(File.ReadAllText(path), path, options, output);
	}

	public static ShaderResult FromString(string effectSource, string filePath, Options options, IEffectCompilerOutput output)
	{
		Dictionary<string, string> macros = new Dictionary<string, string>();
		macros.Add("MGFX", "1");
		options.Profile.AddMacros(macros);
		if (options.Debug)
		{
			macros.Add("DEBUG", "1");
		}
		if (!string.IsNullOrEmpty(options.Defines))
		{
			string[] array = options.Defines.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string define in array)
			{
				string name = define;
				string value = "1";
				if (define.Contains("="))
				{
					string[] parts = define.Split('=');
					if (parts.Length != 0)
					{
						name = parts[0].Trim();
					}
					if (parts.Length > 1)
					{
						value = parts[1].Trim();
					}
				}
				macros.Add(name, value);
			}
		}
		string fullPath = Path.GetFullPath(filePath);
		List<string> dependencies = new List<string>();
		string newFile = Preprocessor.Preprocess(effectSource, fullPath, macros, dependencies, output);
		ParseTree tree = new Parser(new Scanner()).Parse(newFile, fullPath);
		if (tree.Errors.Count > 0)
		{
			string errors = string.Empty;
			foreach (ParseError error in tree.Errors)
			{
				errors += $"{error.File}({error.Line},{error.Column}) : {error.Message}\r\n";
			}
			throw new Exception(errors);
		}
		ShaderInfo shaderInfo = tree.Eval() as ShaderInfo;
		string cleanFile = newFile;
		WhitespaceNodes(TokenType.Technique_Declaration, tree.Nodes, ref cleanFile);
		WhitespaceNodes(TokenType.Sampler_Declaration_States, tree.Nodes, ref cleanFile);
		ShaderResult result = new ShaderResult();
		result.ShaderInfo = shaderInfo;
		result.Dependencies = dependencies;
		result.FilePath = fullPath;
		result.FileContent = cleanFile;
		if (!string.IsNullOrEmpty(options.OutputFile))
		{
			result.OutputFilePath = Path.GetFullPath(options.OutputFile);
		}
		result.AdditionalOutputFiles = new List<string>();
		for (int i = 0; i < shaderInfo.Techniques.Count; i++)
		{
			if (shaderInfo.Techniques[i].Passes.Count <= 0)
			{
				shaderInfo.Techniques.RemoveAt(i);
				i--;
			}
		}
		if (shaderInfo.Techniques.Count <= 0)
		{
			throw new Exception("The effect must contain at least one technique and pass!");
		}
		result.Profile = options.Profile;
		result.Debug = options.Debug;
		return result;
	}

	public static void WhitespaceNodes(TokenType type, List<ParseNode> nodes, ref string sourceFile)
	{
		for (int i = 0; i < nodes.Count; i++)
		{
			ParseNode j = nodes[i];
			if (j.Token.Type != type)
			{
				WhitespaceNodes(type, j.Nodes, ref sourceFile);
				continue;
			}
			int start = j.Token.StartPos;
			int end = j.Token.EndPos;
			int length = end - j.Token.StartPos;
			string content = sourceFile.Substring(start, length);
			for (int c = 0; c < length; c++)
			{
				if (!char.IsWhiteSpace(content[c]))
				{
					content = content.Replace(content[c], ' ');
				}
			}
			string newfile = sourceFile.Substring(0, start);
			newfile += content;
			newfile = (sourceFile = newfile + sourceFile.Substring(end));
		}
	}
}

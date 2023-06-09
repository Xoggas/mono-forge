using System.Collections.Generic;
using System.IO;
using System.Text;
using CppNet;

namespace MGFXC.Effect;

public static class Preprocessor
{
	private class MGFileSystem : VirtualFileSystem
	{
		private readonly List<string> _dependencies;

		public MGFileSystem(List<string> dependencies)
		{
			_dependencies = dependencies;
		}

		public VirtualFile getFile(string path)
		{
			return new MGFile(path, _dependencies);
		}

		public VirtualFile getFile(string dir, string name)
		{
			return new MGFile(Path.Combine(dir, name), _dependencies);
		}
	}

	private class MGFile : VirtualFile
	{
		private readonly List<string> _dependencies;

		private readonly string _path;

		public MGFile(string path, List<string> dependencies)
		{
			_dependencies = dependencies;
			_path = Path.GetFullPath(path);
		}

		public bool isFile()
		{
			if (File.Exists(_path))
			{
				return !File.GetAttributes(_path).HasFlag(FileAttributes.Directory);
			}
			return false;
		}

		public string getPath()
		{
			return _path;
		}

		public string getName()
		{
			return Path.GetFileName(_path);
		}

		public VirtualFile getParentFile()
		{
			return new MGFile(Path.GetDirectoryName(_path), _dependencies);
		}

		public VirtualFile getChildFile(string name)
		{
			return new MGFile(Path.Combine(_path, name), _dependencies);
		}

		public Source getSource()
		{
			if (!_dependencies.Contains(_path))
			{
				_dependencies.Add(_path);
			}
			return new MGStringLexerSource(AppendNewlineIfNonePresent(File.ReadAllText(_path)), ppvalid: true, _path);
		}

		private static string AppendNewlineIfNonePresent(string text)
		{
			if (!text.EndsWith("\n"))
			{
				return text + "\n";
			}
			return text;
		}
	}

	private class MGStringLexerSource : StringLexerSource
	{
		public string Path { get; private set; }

		public MGStringLexerSource(string str, bool ppvalid, string fileName)
			: base(str.Replace("\r\n", "\n"), ppvalid, fileName)
		{
			Path = fileName;
		}
	}

	private class MGErrorListener : PreprocessorListener
	{
		private readonly IEffectCompilerOutput _output;

		public MGErrorListener(IEffectCompilerOutput output)
		{
			_output = output;
		}

		public void handleWarning(Source source, int line, int column, string msg)
		{
			_output.WriteWarning(GetPath(source), line, column, msg);
		}

		public void handleError(Source source, int line, int column, string msg)
		{
			_output.WriteError(GetPath(source), line, column, msg);
		}

		private string GetPath(Source source)
		{
			return ((MGStringLexerSource)source).Path;
		}

		public void handleSourceChange(Source source, string ev)
		{
		}
	}

	public static string Preprocess(string effectCode, string filePath, IDictionary<string, string> defines, List<string> dependencies, IEffectCompilerOutput output)
	{
		string fullPath = Path.GetFullPath(filePath);
		CppNet.Preprocessor pp = new CppNet.Preprocessor();
		pp.EmitExtraLineInfo = false;
		pp.addFeature(Feature.LINEMARKERS);
		pp.setListener(new MGErrorListener(output));
		pp.setFileSystem(new MGFileSystem(dependencies));
		pp.setQuoteIncludePath(new List<string> { Path.GetDirectoryName(fullPath) });
		foreach (KeyValuePair<string, string> define in defines)
		{
			pp.addMacro(define.Key, define.Value);
		}
		effectCode = effectCode.Replace("#line", "//--WORKAROUND#line");
		pp.addInput(new MGStringLexerSource(effectCode, ppvalid: true, fullPath));
		StringBuilder result = new StringBuilder();
		bool endOfStream = false;
		while (!endOfStream)
		{
			Token token = pp.token();
			switch (token.getType())
			{
			case 265:
				endOfStream = true;
				break;
			case 261:
				if (token.getText().StartsWith("//--WORKAROUND#line"))
				{
					result.Append(token.getText().Replace("//--WORKAROUND#line", "#line"));
				}
				break;
			case 260:
			{
				string tokenText = token.getText();
				if (tokenText == null)
				{
					break;
				}
				string text = tokenText;
				foreach (char c in text)
				{
					if (c == '\n')
					{
						result.Append(c);
					}
				}
				break;
			}
			default:
			{
				string tokenText2 = token.getText();
				if (tokenText2 != null)
				{
					result.Append(tokenText2);
				}
				break;
			}
			}
		}
		return result.ToString();
	}
}

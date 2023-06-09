using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MGFXC.Effect;

public class CommandLineParser
{
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class RequiredAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Field)]
	public class NameAttribute : Attribute
	{
		public string Name { get; private set; }

		public string Description { get; protected set; }

		public NameAttribute(string name)
		{
			Name = name;
			Description = null;
		}

		public NameAttribute(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}

	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ProfileNameAttribute : NameAttribute
	{
		public ProfileNameAttribute()
			: base("Profile")
		{
			IEnumerable<string> names = ShaderProfile.All.Select((ShaderProfile p) => p.Name);
			base.Description = "\t - Must be one of the following: " + string.Join(", ", names);
		}
	}

	private object _optionsObject;

	private Queue<FieldInfo> _requiredOptions = new Queue<FieldInfo>();

	private Dictionary<string, FieldInfo> _optionalOptions = new Dictionary<string, FieldInfo>();

	private List<string> _requiredUsageHelp = new List<string>();

	private List<string> _optionalUsageHelp = new List<string>();

	public string Title { get; set; }

	public CommandLineParser(object optionsObject)
	{
		_optionsObject = optionsObject;
		FieldInfo[] fields = optionsObject.GetType().GetFields();
		foreach (FieldInfo field in fields)
		{
			string description;
			string fieldName = GetOptionNameAndDescription(field, out description);
			if (GetAttribute<RequiredAttribute>(field) != null)
			{
				_requiredOptions.Enqueue(field);
				_requiredUsageHelp.Add($"<{fieldName}> {description}");
				continue;
			}
			_optionalOptions.Add(fieldName.ToLowerInvariant(), field);
			if (field.FieldType == typeof(bool))
			{
				_optionalUsageHelp.Add($"/{fieldName} {description}");
			}
			else
			{
				_optionalUsageHelp.Add($"/{fieldName}:value {description}");
			}
		}
	}

	public bool ParseCommandLine(string[] args)
	{
		foreach (string arg in args)
		{
			if (!ParseArgument(arg.Trim()))
			{
				return false;
			}
		}
		FieldInfo missingRequiredOption = _requiredOptions.FirstOrDefault((FieldInfo field) => !IsList(field) || GetList(field).Count == 0);
		if (missingRequiredOption != null)
		{
			ShowError("Missing argument '{0}'", GetOptionName(missingRequiredOption));
			return false;
		}
		return true;
	}

	private bool ParseArgument(string arg)
	{
		if (_requiredOptions.Count > 0)
		{
			FieldInfo field2 = _requiredOptions.Peek();
			if (!IsList(field2))
			{
				_requiredOptions.Dequeue();
			}
			return SetOption(field2, arg);
		}
		if (arg.StartsWith("/"))
		{
			_requiredOptions.Clear();
			char[] separators = new char[1] { ':' };
			string[] split = arg.Substring(1).Split(separators, 2, StringSplitOptions.None);
			string name = split[0];
			string value = ((split.Length > 1) ? split[1] : "true");
			if (!_optionalOptions.TryGetValue(name.ToLowerInvariant(), out var field))
			{
				ShowError("Unknown option '{0}'", name);
				return false;
			}
			return SetOption(field, value);
		}
		ShowError("Too many arguments");
		return false;
	}

	private bool SetOption(FieldInfo field, string value)
	{
		try
		{
			if (IsList(field))
			{
				GetList(field).Add(ChangeType(value, ListElementType(field)));
			}
			else
			{
				field.SetValue(_optionsObject, ChangeType(value, field.FieldType));
			}
			return true;
		}
		catch
		{
			ShowError("Invalid value '{0}' for option '{1}'", value, GetOptionName(field));
			return false;
		}
	}

	private static object ChangeType(string value, Type type)
	{
		return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(value);
	}

	private static bool IsList(FieldInfo field)
	{
		return typeof(IList).IsAssignableFrom(field.FieldType);
	}

	private IList GetList(FieldInfo field)
	{
		return (IList)field.GetValue(_optionsObject);
	}

	private static Type ListElementType(FieldInfo field)
	{
		return (from i in field.FieldType.GetInterfaces()
			where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
			select i).First().GetGenericArguments()[0];
	}

	private static string GetOptionName(FieldInfo field)
	{
		NameAttribute nameAttribute = GetAttribute<NameAttribute>(field);
		if (nameAttribute != null)
		{
			return nameAttribute.Name;
		}
		return field.Name;
	}

	private static string GetOptionNameAndDescription(FieldInfo field, out string description)
	{
		NameAttribute nameAttribute = GetAttribute<NameAttribute>(field);
		if (nameAttribute != null)
		{
			description = nameAttribute.Description;
			return nameAttribute.Name;
		}
		description = null;
		return field.Name;
	}

	private void ShowError(string message, params object[] args)
	{
		string name = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName);
		if (!string.IsNullOrEmpty(Title))
		{
			Console.Error.WriteLine(Title);
			Console.Error.WriteLine();
		}
		Console.Error.WriteLine(message, args);
		Console.Error.WriteLine();
		Console.Error.WriteLine("Usage: {0} {1}", name, string.Join(" ", _requiredUsageHelp));
		if (_optionalUsageHelp.Count <= 0)
		{
			return;
		}
		Console.Error.WriteLine();
		Console.Error.WriteLine("Options:");
		foreach (string optional in _optionalUsageHelp)
		{
			Console.Error.WriteLine("    {0}", optional);
		}
	}

	private static T GetAttribute<T>(ICustomAttributeProvider provider) where T : Attribute
	{
		return provider.GetCustomAttributes(typeof(T), inherit: false).OfType<T>().FirstOrDefault();
	}
}

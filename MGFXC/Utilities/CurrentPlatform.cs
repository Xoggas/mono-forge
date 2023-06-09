using System;
using System.Runtime.InteropServices;

namespace MGFXC.Framework.Utilities;

internal static class CurrentPlatform
{
	private static bool _init;

	private static OS _os;

	public static OS OS
	{
		get
		{
			Init();
			return _os;
		}
	}

	public static string Rid
	{
		get
		{
			if (OS == OS.Windows && Environment.Is64BitProcess)
			{
				return "win-x64";
			}
			if (OS == OS.Windows && !Environment.Is64BitProcess)
			{
				return "win-x86";
			}
			if (OS == OS.Linux)
			{
				return "linux-x64";
			}
			if (OS == OS.MacOSX)
			{
				return "osx";
			}
			return "unknown";
		}
	}

	[DllImport("libc")]
	private static extern int uname(IntPtr buf);

	private static void Init()
	{
		if (_init)
		{
			return;
		}
		switch (Environment.OSVersion.Platform)
		{
		case PlatformID.Win32S:
		case PlatformID.Win32Windows:
		case PlatformID.Win32NT:
		case PlatformID.WinCE:
			_os = OS.Windows;
			break;
		case PlatformID.MacOSX:
			_os = OS.MacOSX;
			break;
		case PlatformID.Unix:
		{
			_os = OS.MacOSX;
			IntPtr buf = IntPtr.Zero;
			try
			{
				buf = Marshal.AllocHGlobal(8192);
				if (uname(buf) == 0 && Marshal.PtrToStringAnsi(buf) == "Linux")
				{
					_os = OS.Linux;
				}
			}
			catch
			{
			}
			finally
			{
				if (buf != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(buf);
				}
			}
			break;
		}
		default:
			_os = OS.Unknown;
			break;
		}
		_init = true;
	}
}

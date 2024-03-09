using System;
using System.IO;
using System.Runtime.InteropServices;
using FmodForFoxes;

namespace MonoGine.Audio;

public class DesktopNativeFmodLibrary : INativeFmodLibrary
{
    public void Init(FmodInitMode mode, bool loggingEnabled = false)
    {
        NativeLibrary.SetDllImportResolver(mode.GetType().Assembly, (libraryName, assembly, dllImportSearchPath) =>
        {
            libraryName = Path.GetFileNameWithoutExtension(libraryName);
            dllImportSearchPath ??= DllImportSearchPath.AssemblyDirectory;

            return NativeLibrary.Load(SelectDefaultLibraryName(libraryName, false), assembly, dllImportSearchPath);
        });
    }

    private static string SelectDefaultLibraryName(string libName, bool loggingEnabled = false)
    {
        if (OperatingSystem.IsWindows())
        {
            if (Environment.Is64BitOperatingSystem)
            {
                return loggingEnabled
                    ? $"runtimes/win-x64/native/{libName}L.dll"
                    : $"runtimes/win-x64/native/{libName}.dll";
            }

            return loggingEnabled
                ? $"runtimes/win-x86/native/{libName}L.dll"
                : $"runtimes/win-x86/native/{libName}.dll";
        }

        if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
        {
            return loggingEnabled
                ? $"runtimes/linux-x64/native/lib{libName}L.so"
                : $"runtimes/linux-x64/native/lib{libName}.so";
        }

        throw new PlatformNotSupportedException();
    }
}
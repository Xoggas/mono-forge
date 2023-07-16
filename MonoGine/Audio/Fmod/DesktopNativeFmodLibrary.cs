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

    private string SelectDefaultLibraryName(string libName, bool loggingEnabled = false)
    {
        string name;

        if (OperatingSystem.IsWindows())
        {
            name = loggingEnabled ? $"{libName}L.dll" : $"{libName}.dll";
        }
        else if (OperatingSystem.IsLinux() || OperatingSystem.IsAndroid())
        {
            name = loggingEnabled ? $"lib{libName}L.so" : $"lib{libName}.so";
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        return name;
    }
}

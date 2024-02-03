using System;
using System.Linq;

namespace MonoGine.Utilities;

internal static class ArrayUtility
{
    internal enum ArrayResizeMode
    {
        MemoryEfficient,
        PerformanceEfficient
    }

    internal static void ResizeArray<T>(ref T[] array, ArrayResizeMode resizeMode = ArrayResizeMode.MemoryEfficient)
    {
        var newSize = resizeMode switch
        {
            ArrayResizeMode.MemoryEfficient => array.Length + array.Length / 2,
            ArrayResizeMode.PerformanceEfficient => array.Length * 2,
            _ => throw new ArgumentOutOfRangeException(nameof(resizeMode), resizeMode, null)
        };

        Array.Resize(ref array, newSize);
    }

    internal static void InitializeElementsInArray<T>(T?[] array) where T : class, new()
    {
        if (array.All(x => x is not null))
        {
            return;
        }

        for (var i = 0; i < array.Length; i++)
        {
            array[i] ??= new T();
        }
    }

    internal static void ExtendArrayIfNeeded<T>(ref T[] array, int minLength,
        ArrayResizeMode resizeMode = ArrayResizeMode.MemoryEfficient)
    {
        while (minLength >= array.Length)
        {
            ResizeArray(ref array, resizeMode);
        }
    }
}
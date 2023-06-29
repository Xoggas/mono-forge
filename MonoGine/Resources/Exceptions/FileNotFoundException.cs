using System;
using System.Runtime.Serialization;

namespace MonoGine.Resources;

public sealed class FileNotFoundException : Exception
{
    public FileNotFoundException()
    {
    }

    public FileNotFoundException(string? message) : base(message)
    {
    }

    public FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FileNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
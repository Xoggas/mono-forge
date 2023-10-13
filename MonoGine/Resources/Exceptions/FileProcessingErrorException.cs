using System;
using System.Runtime.Serialization;

namespace MonoGine.ResourceLoading;

public sealed class FileProcessingErrorException : Exception
{
    public FileProcessingErrorException()
    {
    }

    public FileProcessingErrorException(string? message) : base(message)
    {
    }

    public FileProcessingErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FileProcessingErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
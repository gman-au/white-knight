using System;

namespace White.Knight.Domain.Exceptions
{
    public class UnrecognisedKeyException<T>(Type type)
        : Exception($"Could not use key {type} against repository of type {typeof(T)}");
}
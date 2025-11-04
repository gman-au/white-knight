using System;

namespace White.Knight.Domain.Exceptions
{
    public class ClientSideEvaluationException<T>()
        : Exception($"Could not perform server side evaluation on command of type {typeof(T).Name}");
}
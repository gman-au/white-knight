using System;

namespace White.Knight.Domain.Exceptions
{
    public class MisconfiguredSelectorException(Type type)
        : Exception($"Repository of type {type} has a misconfigured selector expression");
}
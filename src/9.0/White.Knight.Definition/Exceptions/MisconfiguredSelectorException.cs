using System;

namespace White.Knight.Definition.Exceptions
{
    public class MisconfiguredSelectorException(Type type)
        : Exception($"Repository of type {type} has a misconfigured selector expression");
}
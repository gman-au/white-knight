using System;

namespace White.Knight.Domain.Exceptions
{
    public class UnsupportedRepositoryOperationException(string implementation, string method)
        : Exception($"Repository implementation [{implementation}] has no implementation for [{method}]");
}
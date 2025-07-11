using System;

namespace White.Knight.Domain.Exceptions
{
    public class MissingConfigurationException(string configPath)
        : Exception($"Configuration element with path [{configPath}] is missing.");
}
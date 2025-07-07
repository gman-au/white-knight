using System;

namespace White.Knight.Definition.Exceptions
{
    public class MissingConfigurationException(string configPath)
        : Exception($"Configuration element with path [{configPath}] is missing.");
}
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using White.Knight.Abstractions.Options;

namespace White.Knight.Tests.Abstractions.Extensions
{
    public static class ConfigurationEx
    {
        public static IConfigurationRoot ArrangeThrowOnClientSideEvaluation<T>(this IConfigurationRoot configuration)
            where T : RepositoryConfigurationOptions
        {
            var configName = typeof(T).Name;

            var configurationDictionary = new Dictionary<string, string>
            {
                [$"{configName}:ClientSideEvaluationResponse"] = "Throw"
            };

            return
                new ConfigurationBuilder()
                    .AddConfiguration(configuration)
                    .AddInMemoryCollection(configurationDictionary)
                    .Build();
        }
    }
}
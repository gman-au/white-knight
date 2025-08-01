using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Injection.Abstractions;
using White.Knight.InMemory.Attribute;
using White.Knight.InMemory.Options;

namespace White.Knight.InMemory.Injection
{
	public static class ServiceCollectionExtension
	{
        public static IServiceCollection AddInMemoryRepositories(
            this IServiceCollection services, 
            IConfigurationRoot configuration)
        {
            services
                .Configure<InMemoryRepositoryConfigurationOptions>(
                    configuration
                        .GetSection(nameof(InMemoryRepositoryConfigurationOptions))
                );

            services
                .AddSingleton(typeof(ICache<>), typeof(Cache<>));

            return services;
        }

		public static IServiceCollection AddAttributedInMemoryRepositories(
			this IServiceCollection services,
			Assembly repositoryAssembly
		)
        {
            services
                .AddAttributedRepositories<IsInMemoryRepositoryAttribute>(repositoryAssembly);

			return services;
		}

		public static IServiceCollection AddInMemoryRepositoryFeatures(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IInMemoryRepositoryFeatures<>), typeof(InMemoryRepositoryFeatures<>));

			return services;
		}
	}
}
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Abstractions;
using White.Knight.Csv.Attribute;
using White.Knight.Csv.Options;
using White.Knight.Injection.Abstractions;
using White.Knight.Interfaces;

namespace White.Knight.Csv.Injection
{
	public static class ServiceCollectionExtension
	{
        public static IServiceCollection AddCsvRepositories(
            this IServiceCollection services, 
            IConfigurationRoot configuration)
        {
            services
                .Configure<CsvRepositoryConfigurationOptions>(
                    configuration
                        .GetSection(nameof(CsvRepositoryConfigurationOptions))
                );
            
            services
                .AddTransient(typeof(ICsvLoader<>), typeof(CsvLoader<>));

            return services;
        }

		public static IServiceCollection AddAttributedCsvRepositories(
			this IServiceCollection services,
			Assembly repositoryAssembly
		)
		{
			services
				.AddScopedClassesWithAttribute<IsCsvRepositoryAttribute>(
					repositoryAssembly,
					typeof(IRepository<>)
				)
				.AddScopedClassesWithAttribute<IsCsvRepositoryAttribute>(
					repositoryAssembly,
					typeof(IKeylessRepository<>)
				);
            
			return services;
		}

		public static IServiceCollection AddCsvRepositoryOptions(this IServiceCollection services)
		{
			services
				.AddRepositoryExceptionWrapper();

			services
				.AddScoped(typeof(CsvRepositoryOptions<>), typeof(CsvRepositoryOptions<>))
				.AddScoped(typeof(ICsvLoader<>), typeof(CsvLoader<>));

			return services;
		}
        
        private static IServiceCollection AddRepositoryExceptionWrapper(this IServiceCollection services)
        {
            services
                .AddTransient<IRepositoryExceptionWrapper, RepositoryExceptionWrapper>();

            return services;
        }
	}
}
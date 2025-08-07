using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Abstractions.Features;
using White.Knight.Abstractions.Options;
using White.Knight.Interfaces;

namespace White.Knight.Injection.Abstractions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryFeatures<T>(
            this IServiceCollection services,
            IConfigurationRoot configuration)
        where T : RepositoryConfigurationOptions
        {
            services
                .AddTransient<IRepositoryFeatures, RepositoryFeatures>()
                .AddTransient<IClientSideEvaluationHandler, ClientSideEvaluationHandler>();

            services
                .Configure<RepositoryConfigurationOptions>(
                    configuration
                        .GetSection(typeof(T).Name)
                );

            return services;
        }

        public static IServiceCollection AddAttributedRepositories<TA>(
            this IServiceCollection services,
            Assembly repositoryAssembly
        ) where TA : Attribute
        {
            services
                .AddScopedClassesWithAttribute<TA>(
                    repositoryAssembly,
                    typeof(IRepository<>)
                )
                .AddScopedClassesWithAttribute<TA>(
                    repositoryAssembly,
                    typeof(IKeylessRepository<>)
                );

            return services;
        }

        private static IServiceCollection AddScopedClassesWithAttribute<TA>(
            this IServiceCollection services,
            Assembly assembly,
            Type interfaceType
        ) where TA : Attribute
        {
            var attributedClasses =
                assembly
                    .GetTypes()
                    .Where(o => o.GetCustomAttribute<TA>() != null)
                    .ToList();

            foreach (var attributedClass in attributedClasses)
            {
                var implementedInterface =
                    attributedClass
                        .GetInterfaces()
                        .FirstOrDefault(t => t.ImplementsInterface(interfaceType));

                if (implementedInterface?.GenericTypeArguments.Length != 1)
                    continue;

                services
                    .AddScoped(
                        implementedInterface,
                        attributedClass
                    );
            }

            return services;
        }

        public static IServiceCollection AddRepositoryExceptionRethrower(this IServiceCollection services)
        {
            services
                .AddTransient<IRepositoryExceptionRethrower, RepositoryExceptionRethrower>();

            return services;
        }

        private static bool ImplementsInterface(this Type typeToCheck, Type interfaceToCheck)
        {
            return typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == interfaceToCheck;
        }
    }
}
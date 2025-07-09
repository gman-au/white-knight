using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Abstractions.Features;
using White.Knight.Interfaces;

namespace White.Knight.Injection.Abstractions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddScopedClassesWithAttribute<TA>(
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

        public static IServiceCollection AddRepositoryExceptionWrapper(this IServiceCollection services)
        {
            services
                .AddTransient<IRepositoryExceptionWrapper, RepositoryExceptionWrapper>();

            return services;
        }

        private static bool ImplementsInterface(this Type typeToCheck, Type interfaceToCheck)
        {
            return typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == interfaceToCheck;
        }
    }
}
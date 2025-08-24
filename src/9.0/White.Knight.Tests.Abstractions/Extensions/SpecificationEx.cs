using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using White.Knight.Domain;
using White.Knight.Tests.Abstractions.Spec;

namespace White.Knight.Tests.Abstractions.Extensions
{
    public static class SpecificationEx
    {
        public static SpecTypeDiagnostics GetSpecTypeDiagnostics<T>(this Specification<T> specification)
        {
            return GetSpecTypeDiagnostics(specification.GetType());
        }

        public static IEnumerable<SpecTypeDiagnostics> GetSpecs(this Assembly assembly)
        {
            return
                assembly
                    .GetTypes()
                    .Where
                    (
                        t =>
                            t
                                .BaseTypes()
                                .Any
                                (
                                    x => x.IsSpecification()
                                )
                    )
                    .Select
                        (GetSpecTypeDiagnostics);
        }

        public static SpecTypeDiagnostics GetSpecTypeDiagnostics(this Type specType)
        {
            var specInterface =
                specType
                    .BaseTypes()
                    .First
                    (
                        x => x.IsSpecification()
                    );

            return new SpecTypeDiagnostics
            {
                SpecType = specType,
                ImplementedInterface = specInterface,
                SpecTypeParameter = specInterface.GenericTypeArguments[0]
            };
        }

        private static bool ImplementsInterface(this Type typeToCheck, Type interfaceToCheck)
        {
            return typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == interfaceToCheck;
        }

        private static bool IsSpecification(this Type type)
        {
            return type.IsParticularGeneric(typeof(Specification<>));
        }

        private static bool IsParticularGeneric(this Type type, Type generic)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == generic;
        }

        private static IEnumerable<Type> BaseTypes(this Type type)
        {
            var t = type;
            while (true)
            {
                t = t.BaseType;
                if (t == null) break;
                yield return t;
            }
        }
    }
}
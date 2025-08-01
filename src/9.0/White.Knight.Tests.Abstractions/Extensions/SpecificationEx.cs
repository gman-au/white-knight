using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using White.Knight.Domain;
using White.Knight.Tests.Abstractions.Spec;

namespace White.Knight.Tests.Abstractions.Extensions
{
	internal static class SpecificationEx
	{
		public static SpecTypeDetails GetSpecTypeDetails<T>(this Specification<T> specification) =>
			GetSpecTypeDetails(specification.GetType());

		public static IEnumerable<SpecTypeDetails> GetSpecs(this Assembly assembly)
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
						(GetSpecTypeDetails);
		}

		public static SpecTypeDetails GetSpecTypeDetails(this Type specType)
		{
			var specInterface =
				specType
					.BaseTypes()
					.First
					(
                        x => x.IsSpecification()
					);

			return new SpecTypeDetails
			{
				SpecType = specType,
				ImplementedInterface = specInterface,
				SpecTypeParameter = specInterface.GenericTypeArguments[0]
			};
		}

		private static bool ImplementsInterface(this Type typeToCheck, Type interfaceToCheck) =>
			typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == interfaceToCheck;

        private static bool IsSpecification(this Type type) =>
            type.IsParticularGeneric(typeof(Specification<>));

        private static bool IsParticularGeneric(this Type type, Type generic) =>
            type.IsGenericType && type.GetGenericTypeDefinition() == generic;

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
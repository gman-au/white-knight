using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Definition;

namespace White.Knight.Abstractions.Extensions
{
    internal static class MethodCallExpressionEx
    {
        private static readonly IEnumerable<string> ApplicableStringMethods =
        [
            "Contains",
            "StartsWith",
            "EndsWith",
            "Equals"
        ];

        public static ISubQuery ParseMethodCall(this MethodCallExpression methodCallExpression, ref RootQuery rootQuery)
        {
            var result =
                methodCallExpression
                    .ParseStringContains();

            if (result == null)
                result =
                    methodCallExpression
                        .ParseSelfJoin(ref rootQuery);

            return result ?? null;
        }

        private static ISubQuery ParseStringContains(this MethodCallExpression methodCallExpression)
        {
            var reflectedType = methodCallExpression.Method.ReflectedType;
            if (reflectedType != typeof(string)) return null;
            if (methodCallExpression.Arguments.Count != 1) return null;
            if (!ApplicableStringMethods.Contains(methodCallExpression.Method.Name)) return null;
            if (methodCallExpression.Arguments[0] is not MemberExpression memberExpression) return null;

            var propertyName = memberExpression.ExtractMember();

            if (methodCallExpression.Object is not MemberExpression fieldExpression) return null;
            var propertyValue = fieldExpression.ExtractValue();

            var result = new StringSubQuery
            {
                OperandLeft = propertyName,
                Operator = methodCallExpression.Method.Name,
                OperandRight = propertyValue,
                OperandType = typeof(string)
            };

            return result;

        }
    }
}
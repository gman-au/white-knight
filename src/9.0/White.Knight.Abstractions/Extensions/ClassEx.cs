using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace White.Knight.Abstractions.Extensions
{
    public static class ClassEx
    {
        public static T ApplyInclusionStrategy<T>(
            this T sourceEntity,
            T targetEntity,
            Expression<Func<T, object>>[] fieldsToModify,
            Expression<Func<T, object>>[] fieldsToPreserve)
            where T : new()
        {
            var entityToCommit = new T();

            var entityType = typeof(T);

            if (targetEntity == null) return sourceEntity;

            // Set the initial values to the (source) entity
            foreach (var propertyInfo in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value =
                    propertyInfo
                        .GetValue(sourceEntity);

                propertyInfo
                    .SetValue(
                        entityToCommit,
                        value,
                        null
                    );
            }

            ApplyVersionToEntityProperties(
                entityToCommit,
                sourceEntity,
                targetEntity,
                fieldsToModify
            );

            ApplyVersionToEntityProperties(
                entityToCommit,
                targetEntity,
                sourceEntity,
                fieldsToPreserve
            );

            return entityToCommit;
        }

        private static void ApplyVersionToEntityProperties<T>(
            T workingEntity,
            T valuesEntity,
            T fallbackEntity,
            Expression<Func<T, object>>[] fieldsToIterate
        )
        {
            var entityType = typeof(T);

            var modifiedFields = new List<string>();

            if (!(fieldsToIterate ?? Enumerable.Empty<Expression<Func<T, object>>>()).Any())
                return;

            foreach (var field in fieldsToIterate)
            {
                var propertyInfo =
                    ExtractPropertyInfo<T>(field.Body);

                var value =
                    field
                        .Compile()
                        .Invoke(valuesEntity);

                propertyInfo
                    .SetValue(
                        workingEntity,
                        value,
                        null
                    );

                modifiedFields
                    .Add(propertyInfo.Name);
            }

            // Set the remainder to the fallback values
            foreach (var propertyInfo in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                if (!modifiedFields.Contains(propertyInfo.Name))
                {
                    var value =
                        propertyInfo
                            .GetValue(fallbackEntity);

                    propertyInfo
                        .SetValue(
                            workingEntity,
                            value,
                            null
                        );
                }
        }

        public static PropertyInfo ExtractPropertyInfo<T>(Expression fieldBody)
        {
            var entityType = typeof(T);

            PropertyInfo propertyInfo = null;

            if (fieldBody is MemberExpression memberExpression)
            {
                var member =
                    memberExpression
                        .Member;

                propertyInfo =
                    entityType
                        .GetProperty(member.Name);

                return propertyInfo;
            }

            if (fieldBody is UnaryExpression unaryExpression)
                if (unaryExpression.NodeType == ExpressionType.Convert)
                    if (unaryExpression.Operand is MemberExpression convertMemberExpression)
                    {
                        var member =
                            convertMemberExpression
                                .Member;

                        propertyInfo =
                            entityType
                                .GetProperty(member.Name);

                        return propertyInfo;
                    }

            if (propertyInfo == null)
                throw new Exception($"Could not apply iterative field strategy to object of type {fieldBody.NodeType}");

            return propertyInfo;
        }
    }
}
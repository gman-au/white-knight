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

            var modifiedFields = new List<string>();

            // Set the initial values to the entity
            foreach (var propertyInfo in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                if (!modifiedFields.Contains(propertyInfo.Name))
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

            if ((fieldsToModify ?? Enumerable.Empty<Expression<Func<T, object>>>()).Any())
            {
                foreach (var field in fieldsToModify)
                    if (field.Body is MemberExpression memberExpression)
                    {
                        var member =
                            memberExpression
                                .Member;

                        var value =
                            field
                                .Compile()
                                .Invoke(sourceEntity);

                        var propertyInfo =
                            entityType
                                .GetProperty(member.Name);

                        if (propertyInfo == null) continue;

                        propertyInfo
                            .SetValue(
                                entityToCommit,
                                value,
                                null
                            );

                        modifiedFields
                            .Add(propertyInfo.Name);
                    }

                // Set the remainder to the targetEntity values
                foreach (var propertyInfo in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    if (!modifiedFields.Contains(propertyInfo.Name))
                    {
                        var value =
                            propertyInfo
                                .GetValue(targetEntity);

                        propertyInfo
                            .SetValue(
                                entityToCommit,
                                value,
                                null
                            );
                    }
            }

            if ((fieldsToPreserve ?? Enumerable.Empty<Expression<Func<T, object>>>()).Any())
            {
                foreach (var field in fieldsToPreserve)
                    if (field.Body is MemberExpression memberExpression)
                    {
                        var member =
                            memberExpression
                                .Member;

                        var value =
                            field
                                .Compile()
                                .Invoke(targetEntity);

                        var propertyInfo =
                            entityType
                                .GetProperty(member.Name);

                        if (propertyInfo == null) continue;

                        propertyInfo
                            .SetValue(
                                entityToCommit,
                                value,
                                null
                            );

                        modifiedFields
                            .Add(propertyInfo.Name);
                    }

                // Set the remainder to the entity values
                foreach (var propertyInfo in entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    if (!modifiedFields.Contains(propertyInfo.Name))
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
            }

            return entityToCommit;
        }
    }
}
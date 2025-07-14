using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using White.Knight.Abstractions.Definition;

namespace White.Knight.Abstractions.Extensions
{
    public static class SelfJoinExpressionEx
    {
        private const string SelfJoinType = "SelfJoinSpecification`2";
        private const string ChildSpecificationField = "ChildSpecification";
        private const string PropertyField = "Property";
        private const string ToExpressionMethod = "ToExpression";

        public static ISubQuery ParseSelfJoin(this MethodCallExpression methodCallExpression, ref RootQuery rootQuery)
        {
            var arguments =
                methodCallExpression
                    .Arguments;

            if (arguments.Count != 2) return null;
            if (arguments[0] is not MethodCallExpression instanceMethodCallExpression) return null;
            if (instanceMethodCallExpression.Object is not MethodCallExpression selfJoinExpression) return null;
            if (selfJoinExpression.Object is not MemberExpression fieldExpression) return null;
            if (fieldExpression.Expression is not ConstantExpression constantExpression) return null;

            var selfJoinValue =
                constantExpression?
                    .Value;

            var instanceType =
                selfJoinValue?
                    .GetType();

            if (instanceType?.Name != SelfJoinType) return null;

            var specFieldInfo =
                instanceType
                    .GetMember(ChildSpecificationField).First();

            var specificationInstance =
                ((FieldInfo)specFieldInfo)
                .GetValue(selfJoinValue);

            var propertyFieldInfo =
                instanceType
                    .GetMember(PropertyField)
                    .First();

            var propertyInstance =
                ((FieldInfo)propertyFieldInfo)
                .GetValue(selfJoinValue);

            var specificationInstanceType =
                specificationInstance?
                    .GetType();

            var toExpressionMethod =
                specificationInstanceType?
                    .GetMethod(ToExpressionMethod);

            var invokedExpression =
                toExpressionMethod?
                    .Invoke(
                        specificationInstance,
                        []
                    );

            if (invokedExpression is not LambdaExpression lambdaExpression) return null;

            var joinQuery = new RootQuery();

            lambdaExpression
                .BuildRootQuery(ref joinQuery);

            if (joinQuery.Query == null) return null;
            if (propertyInstance is not LambdaExpression propertyExpression) return null;
            if (propertyExpression.Body is not MemberExpression memberExpression) return null;

            var childSet =
                memberExpression
                    .ExtractMember();

            var selfJoin = new SelfJoin
            {
                ChildSet = childSet,
                Alias = null,
                SubQuery = joinQuery.Query
            };

            rootQuery.Joins ??= new List<SelfJoin>();

            rootQuery
                .Joins
                .Add(selfJoin);

            return joinQuery.Query;
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Extensions;
using White.Knight.Interfaces.Spec;

namespace White.Knight.Abstractions.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public static Specification<T>? Empty;

        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public static Specification<T> operator &(Specification<T> a, Specification<T> b)
        {
            return (Specification<T>)a.And(b);
        }

        public static Specification<T> operator |(Specification<T> a, Specification<T> b)
        {
            return (Specification<T>)a.Or(b);
        }

        protected static Expression<Func<T1, T3>> Combine<T1, T2, T3>(
            Expression<Func<T1, T2>> first,
            Expression<Func<T2, T3>> second
        )
        {
            var param =
                Expression
                    .Parameter(typeof(T1));

            var newFirst =
                new
                        ExpressionEx.ReplaceExpressionVisitor(
                            first.Parameters.First(),
                            param
                        )
                    .Visit(first.Body);

            var newSecond =
                new
                        ExpressionEx.ReplaceExpressionVisitor(
                            second.Parameters.First(),
                            newFirst
                        )
                    .Visit(second.Body);

            return
                Expression
                    .Lambda<Func<T1, T3>>(
                        newSecond,
                        param
                    );
        }
    }
}
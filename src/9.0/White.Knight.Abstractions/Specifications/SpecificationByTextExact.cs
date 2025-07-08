using System;
using System.Linq.Expressions;
using White.Knight.Abstractions.Comparators;

namespace White.Knight.Abstractions.Specifications
{
    public class SpecificationByTextExact<T> : SpecificationByTextOperation<T>
    {
        protected SpecificationByTextExact(string value, Expression<Func<T, string>> property) : base(
            value,
            property
        )
        {
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return Combine(
                Property,
                TextExpressions.TextExactMatch(Value)
            );
        }
    }
}
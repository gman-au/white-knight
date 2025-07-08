using System;
using System.Linq.Expressions;
using White.Knight.Abstractions.Comparators;

namespace White.Knight.Abstractions.Specifications
{
    public class SpecificationByTextEndsWith<T> : SpecificationByTextOperation<T>
    {
        protected SpecificationByTextEndsWith(string value, Expression<Func<T, string>> property) : base(
            value,
            property
        )
        {
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return Combine(
                Property,
                TextExpressions.TextEndsWith(Value)
            );
        }
    }
}
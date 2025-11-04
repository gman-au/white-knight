using System;
using System.Linq.Expressions;

namespace White.Knight.Domain
{
    public class SpecificationByEquals<T, TValue>(Expression<Func<T, TValue>> property, TValue value) : Specification<T>
    {
        public Expression<Func<T, TValue>> Property { get; } = property;

        public TValue Value { get; } = value;

        public override bool IsSatisfiedBy(T entity)
        {
            return
                Property
                    .Compile()(entity)
                    .Equals(Value);
        }
    }
}
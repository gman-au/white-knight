using System;
using System.Linq.Expressions;

namespace White.Knight.Domain
{
    public class SpecificationByTextEndsWith<T>(Expression<Func<T, string>> property, string value) : Specification<T>
    {
        public Expression<Func<T, string>> Property { get; } = property;

        public string Value { get; } = value;

        public override bool IsSatisfiedBy(T entity)
        {
            return
                Property
                    .Compile()(entity)
                    .EndsWith(Value);
        }
    }
}
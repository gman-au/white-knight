using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Specifications
{
    public abstract class SpecificationByTextOperation<T>(string value, Expression<Func<T, string>> property)
        : Specification<T>
    {
        protected readonly Expression<Func<T, string>> Property = property;

        protected readonly string Value = value;
    }
}
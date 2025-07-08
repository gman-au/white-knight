using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Specifications
{
    public class SpecificationByAll<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _expression;

        public SpecificationByAll()
        {
            _expression = f => true;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _expression;
        }
    }
}
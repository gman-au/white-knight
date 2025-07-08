using System;
using System.Linq.Expressions;
using White.Knight.Abstractions.Comparators;

namespace White.Knight.Abstractions.Specifications
{
    public class SpecificationByEquals<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _result;

        public SpecificationByEquals(string value, Expression<Func<T, string>> property)
        {
            _result =
                Combine(
                    property,
                    EqualityExpressions.Match(value)
                );
        }

        public SpecificationByEquals(int value, Expression<Func<T, int>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(int? value, Expression<Func<T, int?>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(bool value, Expression<Func<T, bool>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(bool? value, Expression<Func<T, bool?>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(DateTime value, Expression<Func<T, DateTime>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(DateTime? value, Expression<Func<T, DateTime?>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(Guid value, Expression<Func<T, Guid>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public SpecificationByEquals(Guid? value, Expression<Func<T, Guid?>> property)
        {
            _result = Combine(
                property,
                EqualityExpressions.Match(value)
            );
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _result;
        }
    }
}
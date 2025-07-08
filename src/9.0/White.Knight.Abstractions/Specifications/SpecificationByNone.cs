using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Specifications
{
	public class SpecificationByNone<T> : Specification<T>
	{
		private readonly Expression<Func<T, bool>> _expression = f => false;

		public override Expression<Func<T, bool>> ToExpression()
		{
			return _expression;
		}
	}
}
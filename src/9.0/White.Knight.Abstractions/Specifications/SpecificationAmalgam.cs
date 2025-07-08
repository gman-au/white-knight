using System;
using System.Linq.Expressions;

namespace White.Knight.Abstractions.Specifications
{
	public class SpecificationAmalgam<T>(Expression<Func<T, bool>> expression) : Specification<T>
	{
		public override Expression<Func<T, bool>> ToExpression()
		{
			return expression;
		}
	}
}
using System;
using System.Linq.Expressions;

namespace White.Knight.Interfaces.Spec
{
	public interface ISpecification<T>
	{
		Expression<Func<T, bool>> ToExpression();

		bool IsSatisfiedBy(T entity);
	}
}
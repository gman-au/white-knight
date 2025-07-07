using System;
using System.Linq;
using System.Linq.Expressions;

namespace White.Knight.Interfaces
{
	public interface INavigationStrategy<T>
	{
		Expression<Func<IQueryable<T>, IQueryable<T>>> GetStrategy();
	}
}
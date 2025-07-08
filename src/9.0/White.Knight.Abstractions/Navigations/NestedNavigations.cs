using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Navigations
{
	public class NestedNavigations<T> : INavigationStrategy<T> where T : class
	{
		private readonly Expression<Func<IQueryable<T>, IQueryable<T>>> _navigationFunc;

		public NestedNavigations(Expression<Func<IQueryable<T>, IQueryable<T>>> navigationFunc)
		{
			_navigationFunc = navigationFunc;
		}

		public Expression<Func<IQueryable<T>, IQueryable<T>>> GetStrategy() => _navigationFunc;
	}
}
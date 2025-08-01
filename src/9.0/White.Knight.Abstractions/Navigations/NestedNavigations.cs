using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Navigations
{
    public class NestedNavigations<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> navigationFunc)
        : INavigationStrategy<T>
        where T : new()
    {
        public Expression<Func<IQueryable<T>, IQueryable<T>>> GetStrategy()
        {
            return navigationFunc;
        }
    }
}
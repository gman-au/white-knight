using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using White.Knight.Domain;
using White.Knight.Domain.Options;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Extensions
{
    public static class QueryableEx
    {
        public static async Task<RepositoryResult<TP>> ApplyCommandQueryAsync<TD, TP>(
            this IQueryable<TD> set,
            IQueryCommand<TD, TP> command
        )
            where TD : new()
        {
            var specification = command?.Specification;

            var total =
                set
                    .Count(o => specification == null || specification.IsSatisfiedBy(o));

            var results =
                set
                    .BuildQueryable(command)
                    .ToImmutableList();

            return new RepositoryResult<TP>
            {
                Records = results,
                Count = total
            };
        }

        public static Expression EvaluateCommandQuery<TD, TP>(
            this IQueryable<TD> set,
            IQueryCommand<TD, TP> command
        )
            where TD : new()
        {
            var results =
                set
                    .BuildQueryable(command);

            return
                results
                    .Expression;
        }

        private static IQueryable<TP> BuildQueryable<TD, TP>(
            this IQueryable<TD> set,
            IQueryCommand<TD, TP> command
        ) where TD : new()
        {
            var orderBy = command?.PagingOptions?.OrderBy;
            var descending = command?.PagingOptions?.Descending;
            var specification = command?.Specification;
            var navigationStrategy = command?.NavigationStrategy;
            var pagingOptions = command?.PagingOptions;
            var projectionOptions = command?.ProjectionOptions;

            if (specification == null)
                throw new ArgumentNullException
                    (nameof(specification));

            var results =
                set
                    .Where(o => specification.IsSatisfiedBy(o))
                    .AddNavigation(navigationStrategy)
                    .AddSorting(
                        orderBy,
                        descending
                    )
                    .AddPaging(pagingOptions)
                    .AddProjection(projectionOptions);

            return results;
        }

        private static IQueryable<T> AddNavigation<T>(
            this IQueryable<T> queryable,
            INavigationStrategy<T> navigationStrategy
        ) where T : new()
        {
            if (navigationStrategy == null) return queryable;

            var result = navigationStrategy
                .GetStrategy()
                .Compile()
                .Invoke(queryable);

            // Needs to be able to accommodate expressions
            // that return IIncludableQueryable, IThenIncludableQueryable
            if (result is IQueryable<T>)
                queryable = result;

            return queryable;
        }

        private static IQueryable<TOut> AddProjection<TIn, TOut>(
            this IQueryable<TIn> queryable,
            ProjectionOptions<TIn, TOut> projectionOptions
        ) where TIn : new()
        {
            if (projectionOptions?.Projection == null)
                throw new Exception
                    ("Projection not specified for query");

            var projectedQueryable =
                queryable
                    .Select
                        (projectionOptions.Projection)
                    .AsQueryable();

            return projectedQueryable;
        }

        private static IQueryable<T> AddPaging<T>(
            this IQueryable<T> queryable,
            PagingOptions<T> pagingOptions
        ) where T : new()
        {
            if (pagingOptions == null)
                return queryable;

            var pageNumber = pagingOptions.Page ?? 0;
            var pageSize = pagingOptions.PageSize ?? 0;

            if (pageNumber <= 0 || pageSize <= 0)
                return queryable;

            return
                queryable
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .AsQueryable();
        }

        private static IQueryable<TD> AddSorting<TD, TK>(
            this IQueryable<TD> queryable,
            Expression<Func<TD, TK>> orderBy,
            bool? descending
        )
        {
            if (orderBy == null)
                return queryable;

            return (descending ?? false
                    ? queryable.OrderByDescending(orderBy)
                    : queryable.OrderBy(orderBy))
                .AsQueryable();
        }
    }
}
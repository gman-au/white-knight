using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Command;
using White.Knight.Definition.Options;
using White.Knight.Interfaces.Command;
using White.Knight.Interfaces.Spec;

namespace White.Knight.Abstractions.Fluent
{
	public static class FluentQueryEx
	{
		public static IQueryCommand<TD, TD> ToQueryCommand<TD>(this ISpecification<TD> specification) => new QueryCommand<TD, TD>
		{
			Specification = specification,
			ProjectionOptions = new ProjectionOptions<TD, TD> { Projection = o => o }
		};

		public static IQueryCommand<TD, TD> WithPaging<TD>(
			this IQueryCommand<TD, TD> command,
			int? pageNumber,
			int? pageSize
		)
		{
			command.PagingOptions = command?.PagingOptions ?? new PagingOptions<TD>();
			command.PagingOptions.Page = pageNumber;
			command.PagingOptions.PageSize = pageSize;

			return command;
		}

		public static IQueryCommand<TD, TD> WithSorting<TD>(
			this IQueryCommand<TD, TD> command,
			Expression<Func<TD, object>> orderFunc,
			bool? descending
		)
		{
			command.PagingOptions = command?.PagingOptions ?? new PagingOptions<TD>();
			command.PagingOptions.Descending = descending;
			command.PagingOptions.OrderBy = orderFunc;

			return command;
		}

		public static IQueryCommand<TD, TP> WithProjection<TD, TP>(
			this IQueryCommand<TD, TD> command,
			Expression<Func<TD, TP>> projectionFunc
		)
		{
			var projectedCommand = new QueryCommand<TD, TP>
			{
				Specification = command.Specification,
				NavigationStrategy = command.NavigationStrategy,
				PagingOptions = command.PagingOptions,
				ProjectionOptions = new ProjectionOptions<TD, TP> { Projection = projectionFunc }
			};

			return projectedCommand;
		}
        
        public static IQueryCommand<TD, TP> WithStrategy<TD, TP>(
            this IQueryCommand<TD, TP> command,
            Expression<Func<IQueryable<TD>, IQueryable<TD>>> navigation
        )
            where TD : new() =>
            command
                .WithStrategy<TD, IQueryCommand<TD, TP>>(navigation);
	}
}
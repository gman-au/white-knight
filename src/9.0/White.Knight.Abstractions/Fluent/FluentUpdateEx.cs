using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Command;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Fluent
{
	public static class FluentUpdateEx
	{
		public static IUpdateCommand<T> ToUpdateCommand<T>(this T entity) => new UpdateCommand<T>
		{
			Entity = entity
		};

		public static IUpdateCommand<T> OnlyUpdating<T>(
			this IUpdateCommand<T> command,
			params Expression<Func<T, object>>[] inclusions
		) where T : new()
		{
			command.Inclusions = inclusions;
			return command;
		}

		public static IUpdateCommand<T> WithoutUpdating<T>(
			this IUpdateCommand<T> command,
			params Expression<Func<T, object>>[] exclusions
		) where T : new()
		{
			command.Exclusions = exclusions;
			return command;
		}

        public static IUpdateCommand<T> WithStrategy<T>(
            this IUpdateCommand<T> command,
            Expression<Func<IQueryable<T>, IQueryable<T>>> navigation
        )
            where T : new() =>
            command
                .WithStrategy<T, IUpdateCommand<T>>(navigation);
	}
}
using System;
using System.Linq;
using System.Linq.Expressions;
using White.Knight.Abstractions.Navigations;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Fluent
{
	public static class FluentCommandEx
	{
		public static TI WithStrategy<TD, TI>(
			this TI command,
			Expression<Func<IQueryable<TD>, IQueryable<TD>>> navigation
		)
			where TI : ICommand<TD>
			where TD : new()
		{
			if (navigation == null) return command;
			command.NavigationStrategy = new NestedNavigations<TD>(navigation);
			return command;
		}
	}
}
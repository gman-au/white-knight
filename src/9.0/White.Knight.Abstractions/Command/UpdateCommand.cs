using System;
using System.Linq.Expressions;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Command
{
	public class UpdateCommand<T> : IUpdateCommand<T>
	{
		public T Entity { get; set; }

		public INavigationStrategy<T> NavigationStrategy { get; set; }

		public Expression<Func<T, object>>[] Exclusions { get; set; }

		public Expression<Func<T, object>>[] Inclusions { get; set; }
	}
}
using White.Knight.Definition.Options;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;
using White.Knight.Interfaces.Spec;

namespace White.Knight.Abstractions.Command
{
	public class QueryCommand<TD, TP> : IQueryCommand<TD, TP>
	{
		public ISpecification<TD> Specification { get; set; }

		public INavigationStrategy<TD> NavigationStrategy { get; set; }

		public PagingOptions<TD> PagingOptions { get; set; }

		public ProjectionOptions<TD, TP> ProjectionOptions { get; set; }
	}
}
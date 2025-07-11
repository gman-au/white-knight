using White.Knight.Domain.Options;
using White.Knight.Interfaces.Spec;

namespace White.Knight.Interfaces.Command
{
	public interface IQueryCommand<TD, TP> : ICommand<TD>
	{
		public ISpecification<TD> Specification { get; set; }

		public PagingOptions<TD> PagingOptions { get; set; }

		public ProjectionOptions<TD, TP> ProjectionOptions { get; set; }
	}
}
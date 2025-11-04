using White.Knight.Domain;
using White.Knight.Domain.Options;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Abstractions.Command
{
    public class QueryCommand<TD, TP> : IQueryCommand<TD, TP>
    {
        public Specification<TD> Specification { get; set; }

        public INavigationStrategy<TD> NavigationStrategy { get; set; }

        public PagingOptions<TD> PagingOptions { get; set; }

        public ProjectionOptions<TD, TP> ProjectionOptions { get; set; }
    }
}
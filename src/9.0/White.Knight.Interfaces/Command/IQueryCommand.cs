using White.Knight.Domain;
using White.Knight.Domain.Options;

namespace White.Knight.Interfaces.Command
{
    public interface IQueryCommand<TD, TP> : ICommand<TD>
    {
        public Specification<TD> Specification { get; set; }

        public PagingOptions<TD> PagingOptions { get; set; }

        public ProjectionOptions<TD, TP> ProjectionOptions { get; set; }
    }
}
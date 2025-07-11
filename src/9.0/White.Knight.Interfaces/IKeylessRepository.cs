using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Interfaces.Command;

namespace White.Knight.Interfaces
{
	public interface IKeylessRepository<TD>
	{
		Expression<Func<TD, object>> DefaultOrderBy();

		Task<RepositoryResult<TP>> QueryAsync<TP>(
			IQueryCommand<TD, TP> command,
			CancellationToken cancellationToken = default);
	}
}
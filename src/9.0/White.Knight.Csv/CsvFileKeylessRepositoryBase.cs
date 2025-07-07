using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using White.Knight.Definition;
using White.Knight.Definition.Exceptions;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.Csv
{
    public abstract class CsvFileKeylessRepositoryBase<TD>(
        CsvRepositoryOptions<TD> repositoryOptions) : IKeylessRepository<TD>
        where TD : class
    {
        private readonly ICsvLoader<TD> _csvLoader = repositoryOptions.CsvLoader;
        private readonly IRepositoryExceptionWrapper _repositoryExceptionWrapper = repositoryOptions.ExceptionWrapper;

        public abstract Expression<Func<TD, object>> DefaultOrderBy();

        public async Task<RepositoryResult<TP>> QueryAsync<TP>(
            IQueryCommand<TD, TP> command,
            CancellationToken cancellationToken)
        {
            try
            {
                var queryable =
                    await
                        _csvLoader
                            .LoadAsync(cancellationToken);

                var spec =
                    command
                        .Specification;

                var projectionFunc =
                    (command
                         .ProjectionOptions?
                         .Projection ??
                     throw new UndefinedProjectionException()
                    )
                    .Compile();

                var results =
                    queryable
                        .Where(o => spec.IsSatisfiedBy(o))
                        .Select(o => projectionFunc.Invoke(o))
                        .ToArray();

                return new RepositoryResult<TP>
                {
                    Records = results,
                    Count = 0
                };
            }
            catch (Exception e)
            {
                throw RethrowRepositoryException(e);
            }
        }

        private Exception RethrowRepositoryException(Exception exception)
        {
            return _repositoryExceptionWrapper != null
                ? _repositoryExceptionWrapper.Rethrow(exception)
                : exception;
        }
    }
}
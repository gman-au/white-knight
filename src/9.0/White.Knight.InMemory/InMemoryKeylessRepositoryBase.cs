using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using White.Knight.Abstractions.Extensions;
using White.Knight.Domain;
using White.Knight.InMemory.Options;
using White.Knight.InMemory.Translator;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.InMemory
{
    public abstract class InMemoryKeylessRepositoryBase<TD>(
        IInMemoryRepositoryFeatures<TD> repositoryFeatures) : IKeylessRepository<TD>
        where TD : new()
    {
        private readonly IRepositoryExceptionRethrower _repositoryExceptionRethrower = repositoryFeatures.ExceptionRethrower;
        protected readonly ICache<TD> Cache = repositoryFeatures.Cache;
        protected readonly ICommandTranslator<TD, InMemoryTranslationResult> CommandTranslator = repositoryFeatures.CommandTranslator;
        protected readonly ILogger Logger = repositoryFeatures.LoggerFactory.CreateLogger<InMemoryKeylessRepositoryBase<TD>>();
        protected readonly Stopwatch Stopwatch = new();

        public abstract Expression<Func<TD, object>> DefaultOrderBy();

        public async Task<RepositoryResult<TP>> QueryAsync<TP>(
            IQueryCommand<TD, TP> command,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger
                    .LogDebug("Querying records of type [{type}]", typeof(TD).Name);

                Stopwatch
                    .Restart();

                /*
                 * For example purposes only. This InMemory implementation only operates on the expression;
                 * however, some implementations may operate on the command itself, and thus return a specific
                 * response, validating the command if required.
                 */
                var translationResult =
                    CommandTranslator.Translate(command);

                var queryable =
                    await
                        Cache
                            .ReadAsync(cancellationToken);

                var results =
                    await
                        queryable
                            .ApplyCommandQueryAsync(command);

                Logger
                    .LogDebug("Queried records of type [{type}] in {ms} ms", typeof(TD).Name, Stopwatch.ElapsedMilliseconds);

                return results;
            }
            catch (Exception e)
            {
                Logger
                    .LogError("Error querying records of type [{type}]: {error}", typeof(TD).Name, e.Message);

                throw RethrowRepositoryException(e);
            }
            finally
            {
                Stopwatch
                    .Stop();
            }
        }

        protected Exception RethrowRepositoryException(Exception exception)
        {
            return _repositoryExceptionRethrower != null
                ? _repositoryExceptionRethrower.Rethrow(exception)
                : exception;
        }
    }
}
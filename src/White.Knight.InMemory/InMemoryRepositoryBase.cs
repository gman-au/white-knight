using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using White.Knight.Abstractions.Extensions;
using White.Knight.Abstractions.Fluent;
using White.Knight.InMemory.Options;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.InMemory
{
    public abstract class InMemoryRepositoryBase<TD>(
        IInMemoryRepositoryFeatures<TD> repositoryFeatures)
        : InMemoryKeylessRepositoryBase<TD>(repositoryFeatures), IRepository<TD>
        where TD : new()
    {
        public override Expression<Func<TD, object>> DefaultOrderBy()
        {
            return KeyExpression();
        }

        public abstract Expression<Func<TD, object>> KeyExpression();

        public virtual async Task<TD> SingleRecordAsync(object key, CancellationToken cancellationToken)
        {
            return await
                SingleRecordAsync(
                    key
                        .ToSingleRecordCommand<TD>(),
                    cancellationToken
                );
        }

        public async Task<TD> SingleRecordAsync(ISingleRecordCommand<TD> command, CancellationToken cancellationToken)
        {
            var key = command.Key;

            try
            {
                Logger
                    .LogDebug("Retrieving single record with key [{key}]", key);

                Stopwatch
                    .Restart();

                var selector =
                    key
                        .BuildKeySelectorExpression(KeyExpression());

                var inMemoryEntity =
                    (await
                        Cache
                            .ReadAsync(cancellationToken))
                    .FirstOrDefault(selector.Compile());

                Logger
                    .LogDebug("Retrieved single record with key [{key}] in {ms} ms", key,
                        Stopwatch.ElapsedMilliseconds);

                return inMemoryEntity;
            }
            catch (Exception e)
            {
                Logger
                    .LogError("Retrieving single record with key [{key}]: {error}", key, e.Message);

                throw RethrowRepositoryException(e);
            }
            finally
            {
                Stopwatch
                    .Stop();
            }
        }

        public virtual async Task<TD> AddOrUpdateAsync(
            IUpdateCommand<TD> command,
            CancellationToken cancellationToken = default)
        {
            return await AddOrUpdateWithModifiedAsync(
                command.Entity,
                command.Inclusions,
                command.Exclusions,
                cancellationToken
            );
        }

        public async Task<object> DeleteRecordAsync(
            ISingleRecordCommand<TD> command,
            CancellationToken cancellationToken)
        {
            var key = command.Key;

            try
            {
                Logger
                    .LogDebug("Deleting record with key [{key}]", key);

                Stopwatch
                    .Restart();

                await
                    Cache
                        .RemoveAsync(key, cancellationToken);

                Logger
                    .LogDebug("Deleted record with key [{key}] in {ms} ms", key, Stopwatch.ElapsedMilliseconds);

                return key;
            }
            catch (Exception e)
            {
                Logger
                    .LogError("Error deleting record key [{key}]: {error}", key, e.Message);

                throw RethrowRepositoryException(e);
            }
            finally
            {
                Stopwatch
                    .Stop();
            }
        }

        private async Task<TD> AddOrUpdateWithModifiedAsync(
            TD sourceEntity,
            Expression<Func<TD, object>>[] fieldsToModify,
            Expression<Func<TD, object>>[] fieldsToPreserve,
            CancellationToken cancellationToken
        )
        {
            TD entityToCommit;

            try
            {
                Logger
                    .LogDebug("Upserting record of type [{type}]", typeof(TD).Name);

                Stopwatch
                    .Restart();

                var selector =
                    sourceEntity
                        .BuildEntitySelectorExpression(KeyExpression());

                var allInMemoryEntities =
                    (await
                        Cache
                            .ReadAsync(cancellationToken))
                    .ToList();

                var targetEntity =
                    allInMemoryEntities
                        .FirstOrDefault(o => selector.Compile().Invoke(o));

                entityToCommit =
                    sourceEntity
                        .ApplyInclusionStrategy(
                            targetEntity,
                            fieldsToModify,
                            fieldsToPreserve);

                object key;
                if (targetEntity == null)
                    // For example purposes only - this would not work in practice!
                    key =
                        Guid
                            .NewGuid();
                else
                    key =
                        KeyExpression()
                            .Compile()
                            .Invoke(targetEntity);

                await
                    Cache
                        .WriteAsync(key, entityToCommit, cancellationToken);

                Logger
                    .LogDebug("Upserted record of type [{type}] in {ms} ms", typeof(TD).Name,
                        Stopwatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                Logger
                    .LogError("Error upserting record of type [{type}]: {error}", typeof(TD).Name, e.Message);

                throw RethrowRepositoryException(e);
            }
            finally
            {
                Stopwatch
                    .Stop();
            }

            return entityToCommit;
        }
    }
}
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Features
{
    public class RepositoryFeatures(
        IClientSideEvaluationHandler clientSideEvaluationHandler,
        IRepositoryExceptionRethrower exceptionRethrower = null,
        ILoggerFactory loggerFactory = null)
        : IRepositoryFeatures
    {
        public IClientSideEvaluationHandler ClientSideEvaluationHandler { get; set; } = clientSideEvaluationHandler;

        public IRepositoryExceptionRethrower ExceptionRethrower { get; set; } = exceptionRethrower;

        public ILoggerFactory LoggerFactory { get; set; } = loggerFactory ?? new NullLoggerFactory();
    }
}
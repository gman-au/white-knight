using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Features
{
    public class RepositoryFeatures(
        IRepositoryExceptionRethrower exceptionRethrower = null,
        ILoggerFactory loggerFactory = null)
        : IRepositoryFeatures
    {
        public IRepositoryExceptionRethrower ExceptionRethrower { get; set; } = exceptionRethrower;

        public ILoggerFactory LoggerFactory { get; set; } = loggerFactory ?? new NullLoggerFactory();
    }
}
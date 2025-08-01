using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using White.Knight.InMemory.Translator;
using White.Knight.Interfaces;

namespace White.Knight.InMemory.Options
{
    public class InMemoryRepositoryFeatures<TD>(
        ICache<TD> cache,
        ICommandTranslator<TD, InMemoryTranslationResult> commandTranslator,
        IRepositoryExceptionRethrower exceptionRethrower = null,
        ILoggerFactory loggerFactory = null)
        : IInMemoryRepositoryFeatures<TD>
    {
        public ICache<TD> Cache { get; set; } = cache;

        public ICommandTranslator<TD, InMemoryTranslationResult> CommandTranslator { get; set; } = commandTranslator;

        public IRepositoryExceptionRethrower ExceptionRethrower { get; set; } = exceptionRethrower;

        public ILoggerFactory LoggerFactory { get; set; } = loggerFactory ?? new NullLoggerFactory();
    }
}
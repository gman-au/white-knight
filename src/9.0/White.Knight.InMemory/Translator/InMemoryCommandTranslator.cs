using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using White.Knight.Domain;
using White.Knight.Domain.Exceptions;
using White.Knight.Interfaces;
using White.Knight.Interfaces.Command;

namespace White.Knight.InMemory.Translator
{
    public class InMemoryCommandTranslator<TD, TResponse>(
        ILoggerFactory loggerFactory = null
    )
        : ICommandTranslator<TD, InMemoryTranslationResult>
    {
        private readonly ILogger _logger =
            (loggerFactory ?? new NullLoggerFactory())
            .CreateLogger<InMemoryCommandTranslator<TD, TResponse>>();

        public InMemoryTranslationResult Translate(ISingleRecordCommand<TD> command)
        {
            var query = $"GET SINGLE RECORD OF {typeof(TD).Name.ToUpper()} WITH AN EXAMPLE UPDATE";

            _logger
                .LogDebug("Translated Query: [{query}]", query);

            return new InMemoryTranslationResult
            {
                ExampleQueryLanguageString = query
            };
        }

        public InMemoryTranslationResult Translate<TP>(IQueryCommand<TD, TP> command)
        {
            var specification = command.Specification;

            if (specification is SpecificationThatIsNotCompatible<TD>)
                throw new UnparsableSpecificationException();

            var query = $"RUN QUERY OF {typeof(TD).Name.ToUpper()} FILTER BY X Y Z";

            _logger
                .LogDebug("Translated Query: ({specification}) [{query}]", specification.GetType().Name, query);

            return new InMemoryTranslationResult
            {
                ExampleQueryLanguageString = query
            };
        }

        public InMemoryTranslationResult Translate(IUpdateCommand<TD> command)
        {
            var query = $"UPDATE {typeof(TD).Name.ToUpper()} WITH AN EXAMPLE UPDATE";

            _logger
                .LogDebug("Translated Query: [{query}]", query);

            return new InMemoryTranslationResult
            {
                ExampleQueryLanguageString = query
            };
        }
    }
}
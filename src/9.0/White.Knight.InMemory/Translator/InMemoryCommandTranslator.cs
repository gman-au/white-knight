using System;
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

            try
            {
                var query = $"RUN QUERY OF {typeof(TD).Name.ToUpper()} {Translate(specification)} FILTER BY X Y Z";

                _logger
                    .LogDebug("Translated Query: ({specification}) [{query}]", specification.GetType().Name, query);

                return new InMemoryTranslationResult
                {
                    ExampleQueryLanguageString = query
                };
            }
            catch (Exception e) when (e is NotImplementedException or UnparsableSpecificationException)
            {
                _logger
                    .LogDebug("Error translating Query: ({specification})", specification.GetType().Name);

                throw;
            }
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

        private string Translate(Specification<TD> spec)
        {
            return spec switch
            {
                SpecificationByAll<TD> => " TRUE ",
                SpecificationByNone<TD> => " FALSE ",
                SpecificationByEquals<TD, string> eq => $" {eq.Property.Name} = '{eq.Value}' ",
                SpecificationByEquals<TD, int> eq => $" {eq.Property.Name} = {eq.Value} ",
                SpecificationByEquals<TD, Guid> eq => $" {eq.Property.Name} = {eq.Value} ",
                SpecificationByAnd<TD> and => $"( {Translate(and.Left)} AND {Translate(and.Right)} )",
                SpecificationByOr<TD> or => $"( {Translate(or.Left)} AND {Translate(or.Right)} )",
                SpecificationByNot<TD> not => $"NOT ( {Translate(not.Spec)} )",
                SpecificationByTextStartsWith<TD> text => $" STARTSWITH {text.Value} )",
                SpecificationByTextContains<TD> text => $" CONTAINS {text.Value} )",
                SpecificationThatIsNotCompatible<TD> => throw new UnparsableSpecificationException(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
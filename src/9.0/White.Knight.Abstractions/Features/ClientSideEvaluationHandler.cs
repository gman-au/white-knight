using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using White.Knight.Abstractions.Options;
using White.Knight.Domain.Enum;
using White.Knight.Domain.Exceptions;
using White.Knight.Interfaces;

namespace White.Knight.Abstractions.Features
{
    public class ClientSideEvaluationHandler : IClientSideEvaluationHandler
    {
        private readonly IOptions<RepositoryConfigurationOptions> _options;

        private readonly ILogger _logger;

        public ClientSideEvaluationHandler(
            IOptions<RepositoryConfigurationOptions> options,
            ILoggerFactory loggerFactory = null)
        {
            _options = options;

            _logger  =
                (loggerFactory ?? new NullLoggerFactory())
                .CreateLogger<ClientSideEvaluationHandler>();
        }

        public void Handle<T>()
        {
            var response =
                _options
                    .Value?
                    .ClientSideEvaluationResponse;

            if (response == ClientSideEvaluationResponseTypeEnum.Throw)
                throw new ClientSideEvaluationException<T>();

            if (response == ClientSideEvaluationResponseTypeEnum.Warn)
                _logger
                    .LogWarning("Could not perform server side evaluation on command of type {type}", typeof(T).Name);
        }
    }
}
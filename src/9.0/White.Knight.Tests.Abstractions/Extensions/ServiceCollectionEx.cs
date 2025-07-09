using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;
using Xunit.Extensions.Logging;

namespace White.Knight.Tests.Abstractions.Extensions
{
    public static class ServiceCollectionEx
    {
        public static void ArrangeXunitOutputLogging(
            this IServiceCollection serviceCollection,
            ITestOutputHelper helper)
        {
            var loggerFactory =
                new LoggerFactory([new XunitLoggerProvider(helper, (s, level) => true)]);

            serviceCollection
                .AddSingleton(typeof(ILoggerFactory), s => loggerFactory);
        }
    }
}
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using White.Knight.Domain.Enum;
using White.Knight.Injection.Abstractions;
using White.Knight.InMemory.Injection;
using White.Knight.InMemory.Options;
using White.Knight.InMemory.Tests.Integration.Repositories;
using White.Knight.Tests.Abstractions;
using White.Knight.Tests.Abstractions.Extensions;
using White.Knight.Tests.Abstractions.Injection;
using White.Knight.Tests.Abstractions.Tests;
using White.Knight.Tests.Domain;

namespace White.Knight.InMemory.Tests.Integration
{
    public class InMemoryInjectionTests() : AbstractedInjectionTests(new InMemoryInjectionTestContext())
    {
        private static readonly Assembly RepositoryAssembly =
            Assembly
                .GetAssembly(typeof(AddressRepository));

        private class InMemoryInjectionTestContext : InjectionTestContextBase, IInjectionTestContext
        {
            public override void ArrangeImplementedServices()
            {
                ServiceCollection
                    .AddInMemoryRepositories(Configuration)
                    .AddAttributedInMemoryRepositories(RepositoryAssembly);

                ServiceCollection
                    .AddRepositoryFeatures<InMemoryRepositoryConfigurationOptions>(Configuration)
                    .AddInMemoryRepositoryFeatures();
            }

            public override void ArrangeDefinedClientSideConfiguration()
            {
                Configuration =
                    Configuration
                        .ArrangeThrowOnClientSideEvaluation<InMemoryRepositoryConfigurationOptions>();
            }

            public override void AssertLoggerFactoryResolved()
            {
                var features =
                    Sut
                        .GetRequiredService<IInMemoryRepositoryFeatures<Address>>();

                Assert
                    .NotNull(features);

                var loggerFactory =
                    features
                        .LoggerFactory;

                Assert
                    .NotNull(loggerFactory);
            }

            public override void AssertRepositoryOptionsResolvedWithDefault()
            {
                var options =
                    Sut
                        .GetRequiredService<IOptions<InMemoryRepositoryConfigurationOptions>>();

                Assert.NotNull(options.Value);

                Assert.Equal(ClientSideEvaluationResponseTypeEnum.Warn, options.Value.ClientSideEvaluationResponse);
            }

            public override void AssertRepositoryOptionsResolvedWithDefined()
            {
                var options =
                    Sut
                        .GetRequiredService<IOptions<InMemoryRepositoryConfigurationOptions>>();

                Assert.NotNull(options.Value);

                Assert.Equal(ClientSideEvaluationResponseTypeEnum.Throw, options.Value.ClientSideEvaluationResponse);
            }
        }
    }
}
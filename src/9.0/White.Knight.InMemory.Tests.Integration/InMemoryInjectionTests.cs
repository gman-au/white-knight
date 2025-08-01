using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Injection.Abstractions;
using White.Knight.InMemory.Injection;
using White.Knight.InMemory.Options;
using White.Knight.InMemory.Tests.Integration.Repositories;
using White.Knight.Tests.Abstractions;
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
                    .AddRepositoryFeatures()
                    .AddInMemoryRepositoryFeatures();
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
        }
    }
}
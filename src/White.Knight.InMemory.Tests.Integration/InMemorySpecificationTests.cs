using System.Reflection;
using White.Knight.InMemory.Injection;
using White.Knight.InMemory.Tests.Integration.Repositories;
using White.Knight.InMemory.Translator;
using White.Knight.Tests.Abstractions;
using White.Knight.Tests.Abstractions.Extensions;
using White.Knight.Tests.Abstractions.Spec;
using White.Knight.Tests.Abstractions.Tests;
using White.Knight.Tests.Domain.Specifications;
using Xunit.Abstractions;

namespace White.Knight.InMemory.Tests.Integration
{
    public class InMemorySpecificationTests(ITestOutputHelper helper)
        : AbstractedSpecificationTests(new InMemorySpecificationTestContext(helper))
    {
        private static readonly Assembly SpecAssembly =
            Assembly
                .GetAssembly(typeof(CustomerSpecByCustomerName));

        private static readonly Assembly RepositoryAssembly =
            Assembly
                .GetAssembly(typeof(AddressRepository));

        private class InMemorySpecificationTestContext : SpecificationTestContextBase<InMemoryTranslationResult>, ISpecificationTestContext
        {
            public InMemorySpecificationTestContext(ITestOutputHelper testOutputHelper)
            {
                SpecificationAssembly = SpecAssembly;

                // specify in memory harness
                LoadTestConfiguration();

                // service initialisation
                ServiceCollection
                    .AddInMemoryRepositories(Configuration)
                    .AddAttributedInMemoryRepositories(RepositoryAssembly);

                // redirect ILogger output to Xunit console
                ServiceCollection
                    .ArrangeXunitOutputLogging(testOutputHelper);

                ServiceCollection
                    .AddInMemoryRepositoryFeatures();

                LoadServiceProvider();
            }
        }
    }
}
using System.Reflection;
using White.Knight.Csv.Injection;
using White.Knight.Tests.Csv.Unit.Repository;
using White.Knights.Tests.Abstractions;
using White.Knights.Tests.Abstractions.Context;
using White.Knights.Tests.Abstractions.Tests;

namespace White.Knight.Tests.Csv.Integration
{
    public class CustomerRepositoryTests() : AbstractedRepositoryTests(new CsvRepositoryTestContext())
    {
        private static readonly Assembly RepositoryAssembly =
            Assembly
                .GetAssembly(typeof(AddressRepository));

        private class CsvRepositoryTestContext : RepositoryTestContextBase, IRepositoryTestContext
        {
            public CsvRepositoryTestContext()
            {
                // specify csv harness
                LoadTestConfiguration<CsvTestHarness>();

                // service initialisation
                ServiceCollection
                    .AddCsvRepositories(Configuration)
                    .AddAttributedCsvRepositories(RepositoryAssembly);

                ServiceCollection
                    .AddCsvRepositoryOptions();

                LoadServiceProvider();
            }
        }
    }
}
using System.Reflection;
using White.Knight.Csv.Injection;
using White.Knight.Tests.Csv.Unit.Repository;
using White.Knights.Tests.Integration;
using TestContextBase = White.Knights.Tests.Integration.Context.TestContextBase;

namespace White.Knight.Tests.Csv.Integration
{
    public class CustomerRepositoryTests() : AbstractedTestSheet(new CsvRepositoryTestContext())
    {
        private static readonly Assembly RepositoryAssembly =
            Assembly
                .GetAssembly(typeof(AddressRepository));

        private class CsvRepositoryTestContext : TestContextBase, ITestContext
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
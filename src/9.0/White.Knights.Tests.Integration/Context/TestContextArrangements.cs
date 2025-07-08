using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;
using White.Knights.Tests.Integration.Data;

namespace White.Knights.Tests.Integration.Context
{
    public partial class TestContextBase
    {
        protected IConfigurationRoot Configuration;
        protected ServiceCollection ServiceCollection;
        protected IServiceProvider ServiceProvider;

        protected void LoadTestConfiguration<T>() where T : class, ITestHarness
        {
            ServiceCollection = [];

            ServiceCollection
                .AddTransient<ITestDataGenerator, TestDataGenerator>()
                .AddTransient<ITestHarness, T>();

            var path =
                Path
                    .GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("Could not get directory name");

            Configuration =
                new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        protected void LoadServiceProvider()
        {
            ServiceProvider =
                ServiceCollection
                    .BuildServiceProvider();

            _sut =
                ServiceProvider
                    .GetRequiredService<IRepository<Customer>>();
        }

        public async Task ArrangeTableDataAsync()
        {
            var testHarness =
                ServiceProvider
                    .GetRequiredService<ITestHarness>();

            await
                testHarness
                    .LoadTableDataAsync();
        }
    }
}
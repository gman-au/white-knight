using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace White.Knights.Tests.Integration
{
    public class TestContextBase
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
        }
    }
}
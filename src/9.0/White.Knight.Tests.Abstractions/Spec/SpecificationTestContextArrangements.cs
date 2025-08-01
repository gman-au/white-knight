using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Abstractions.Spec
{
    public partial class SpecificationTestContextBase<TResponse>
    {
        private IServiceProvider _serviceProvider;
        protected IConfigurationRoot Configuration;
        protected ServiceCollection ServiceCollection;
        protected Assembly SpecificationAssembly;

        protected void LoadTestConfiguration()
        {
            ServiceCollection = [];

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
            _serviceProvider =
                ServiceCollection
                    .BuildServiceProvider();

            Sut =
                _serviceProvider
                    .GetRequiredService<ICommandTranslator<Customer, TResponse>>();
        }
    }
}
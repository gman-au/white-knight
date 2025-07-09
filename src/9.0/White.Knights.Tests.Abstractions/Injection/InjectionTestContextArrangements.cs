using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Injection.Abstractions;

namespace White.Knights.Tests.Abstractions.Injection
{
    public partial class InjectionTestContextBase
    {
        protected IConfigurationRoot Configuration;
        protected readonly ServiceCollection ServiceCollection = [];
        protected IServiceProvider Sut;

        public virtual void ArrangeImplementedServices()
        {
            throw new NotImplementedException("Override this method in your implementation");
        }

        public virtual void ArrangeAppSettingsConfiguration()
        {
            Configuration =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        public virtual void ArrangeAddLogging()
        {
            ServiceCollection
                .AddLogging();
        }

        public virtual void ArrangeInjectExceptionWrapper()
        {
            ServiceCollection
                .AddRepositoryExceptionWrapper();
        }
    }
}
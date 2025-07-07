using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Csv.Unit.Injection
{
	public class BaseResolverTestContext
	{
		protected IServiceProvider ServiceProvider;
		protected IConfigurationRoot Configuration;
		protected ServiceCollection Services;

		public void ArrangeServices()
		{
			Services = [];
		}

		public void ArrangeConfiguration()
		{
			Configuration =
				new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json")
					.Build();
		}

		public void AssertExceptionWrapperResolved()
		{
			Assert.NotNull(ServiceProvider.GetService<IRepositoryExceptionWrapper>());
		}

		public void AssertExceptionWrapperNotResolved()
		{
			Assert.Null(ServiceProvider.GetService<IRepositoryExceptionWrapper>());
		}

		public void AssertRepositoryResolved()
		{
			Assert.NotNull(ServiceProvider.GetService<IRepository<Customer>>());
			Assert.Null(ServiceProvider.GetService<IRepository<Address>>());
			Assert.NotNull(ServiceProvider.GetService<IKeylessRepository<Address>>());
		}
	}
}
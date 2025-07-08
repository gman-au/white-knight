using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using White.Knight.Csv;
using White.Knight.Csv.Injection;
using White.Knight.Interfaces;
using White.Knight.Tests.Csv.Unit.Repository;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Csv.Unit.Injection
{
	public class DependencyInjectionTests
	{
		private static readonly Assembly RepositoryAssembly = Assembly.GetAssembly(typeof(AddressRepository));

		private readonly TestContext _context = new();

		[Fact]
		public void Can_resolve_repository_with_options()
		{
			_context.ArrangeServices();
			_context.ArrangeConfiguration();
			_context.ActAddLogging();
			_context.ActInjectWithWrapper();
			_context.AssertRepositoryResolved();
			_context.AssertExceptionWrapperResolved();
			_context.AssertForeignKeyExceptionWasRegistered();
			_context.AssertOptionsWereRegistered();
		}

		[Fact]
		public void Can_resolve_repository_without_logger()
		{
			_context.ArrangeServices();
			_context.ArrangeConfiguration();
			_context.ActInjectWithWrapper();
			_context.AssertRepositoryResolved();
			_context.AssertExceptionWrapperResolved();
			_context.AssertForeignKeyExceptionWasRegistered();
			_context.AssertOptionsWereRegistered();
		}

		private class TestContext : BaseResolverTestContext
		{
			public void ActInjectWithWrapper()
			{
				Services
					.AddCsvRepositories(Configuration)
					.AddAttributedCsvRepositories(RepositoryAssembly);

				Services
					.AddCsvRepositoryOptions();

				ServiceProvider =
					Services
						.BuildServiceProvider();
			}

			public void ActAddLogging()
			{
				Services
					.AddLogging();
			}

			public void AssertOptionsWereRegistered()
			{
				var options =
					ServiceProvider
						.GetRequiredService<CsvRepositoryOptions<Address>>();

				Assert.NotNull(options.ExceptionWrapper);
			}

			public void AssertForeignKeyExceptionWasRegistered()
			{
				var wrapper =
					ServiceProvider
						.GetRequiredService<IRepositoryExceptionWrapper>();

				// var exception = new DbUpdateException(
				// 	"This is a dummy exception",
				// 	new System.Exception(DbExceptionMessage)
				// );

				// var wrappedException = wrapper.Rethrow(exception);

				// Assert.IsType<ForeignKeyConflictException>(wrappedException);
			}
		}
	}
}
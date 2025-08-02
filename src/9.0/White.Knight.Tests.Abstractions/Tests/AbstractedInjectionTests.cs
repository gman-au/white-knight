using Xunit;

namespace White.Knight.Tests.Abstractions.Tests
{
    public abstract class AbstractedInjectionTests(IInjectionTestContext context)
    {
        protected IInjectionTestContext GetContext() => context;

        [Fact]
        public void Can_resolve_repository_with_options()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeAddLogging();
            context.ArrangeInjectExceptionRethrower();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertExceptionRethrowerResolved();
            context.AssertRepositoryFeaturesResolved();
            context.AssertLoggerFactoryResolved();
        }

        [Fact]
        public void Can_resolve_repository_without_rethrower()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeAddLogging();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertRepositoryFeaturesResolved();
            context.AssertExceptionRethrowerNotResolved();
            context.AssertLoggerFactoryResolved();
        }

        [Fact]
        public void Can_resolve_repository_without_logger()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeInjectExceptionRethrower();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertExceptionRethrowerResolved();
            context.AssertRepositoryFeaturesResolved();
            context.AssertLoggerFactoryResolved();
        }
    }
}
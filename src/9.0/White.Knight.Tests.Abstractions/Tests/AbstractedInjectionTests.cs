using Xunit;

namespace White.Knight.Tests.Abstractions.Tests
{
    public abstract class AbstractedInjectionTests(IInjectionTestContext context)
    {
        [Fact]
        public void Can_resolve_repository_with_options()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeAddLogging();
            context.ArrangeInjectExceptionWrapper();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertExceptionWrapperResolved();
            context.AssertRepositoryOptionsResolved();
            context.AssertLoggerFactoryResolved();
        }

        [Fact]
        public void Can_resolve_repository_without_wrapper()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeAddLogging();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertExceptionWrapperNotResolved();
            context.AssertRepositoryOptionsResolved();
            context.AssertLoggerFactoryResolved();
        }

        [Fact]
        public void Can_resolve_repository_without_logger()
        {
            context.ArrangeAppSettingsConfiguration();
            context.ArrangeImplementedServices();
            context.ArrangeInjectExceptionWrapper();
            context.ActLoadServiceProvider();
            context.AssertRepositoryResolved();
            context.AssertExceptionWrapperResolved();
            context.AssertRepositoryOptionsResolved();
            context.AssertLoggerFactoryResolved();
        }
    }
}
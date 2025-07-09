namespace White.Knight.Tests.Abstractions
{
    public interface IInjectionTestContext
    {
        void ArrangeAppSettingsConfiguration();

        void ArrangeImplementedServices();

        void ArrangeAddLogging();

        void ArrangeInjectExceptionWrapper();

        void ActLoadServiceProvider();

        void AssertRepositoryResolved();

        void AssertExceptionWrapperResolved();

        void AssertExceptionWrapperNotResolved();

        void AssertRepositoryOptionsResolved();

        void AssertLoggerFactoryResolved();
    }
}
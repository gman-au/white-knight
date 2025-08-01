namespace White.Knight.Tests.Abstractions
{
    public interface IInjectionTestContext
    {
        void ArrangeAppSettingsConfiguration();

        void ArrangeImplementedServices();

        void ArrangeAddLogging();

        void ArrangeInjectExceptionRethrower();

        void ActLoadServiceProvider();

        void AssertRepositoryResolved();

        void AssertExceptionRethrowerResolved();

        void AssertExceptionRethrowerNotResolved();

        void AssertRepositoryFeaturesResolved();

        void AssertLoggerFactoryResolved();
    }
}
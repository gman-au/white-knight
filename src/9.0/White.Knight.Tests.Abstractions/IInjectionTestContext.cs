namespace White.Knight.Tests.Abstractions
{
    public interface IInjectionTestContext
    {
        void ArrangeAppSettingsConfiguration();

        void ArrangeImplementedServices();

        void ArrangeAddLogging();

        void ArrangeInjectExceptionRethrower();

        void ArrangeDefinedClientSideConfiguration();

        void ActLoadServiceProvider();

        void AssertRepositoryResolved();

        void AssertExceptionRethrowerResolved();

        void AssertExceptionRethrowerNotResolved();

        void AssertRepositoryFeaturesResolved();

        void AssertBaseRepositoryOptionsResolvedWithDefault();

        void AssertLoggerFactoryResolved();

        void AssertRepositoryOptionsResolvedWithDefault();

        void AssertRepositoryOptionsResolvedWithDefined();

        void AssertBaseRepositoryOptionsResolvedWithDefined();
    }
}
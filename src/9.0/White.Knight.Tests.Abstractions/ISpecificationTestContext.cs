namespace White.Knight.Tests.Abstractions
{
    public interface ISpecificationTestContext
    {
        void ActVerifyTransmutabilityOfAssembly();

        void ActVerifyUntransmutableSpec();

        void ActVerifyNestedUntransmutableSpec();
    }
}
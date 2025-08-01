namespace White.Knight.Tests.Abstractions
{
    public interface ISpecificationTestContext
    {
        bool ActVerifyTransmutabilityOfAssembly();

        void ActVerifyUntransmutableSpec();

        void ActVerifyNestedUntransmutableSpec();
    }
}
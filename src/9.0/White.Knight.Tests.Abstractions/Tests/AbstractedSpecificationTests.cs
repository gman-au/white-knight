using White.Knight.Domain.Exceptions;
using Xunit;

namespace White.Knight.Tests.Abstractions.Tests
{
    public abstract class AbstractedSpecificationTests(ISpecificationTestContext context)
    {
        [SkippableFact]
        public void Can_verify_transmutability_of_all_specifications()
        {
            var result = context.ActVerifyTransmutabilityOfAssembly();

            Skip.IfNot(result);
        }

        [Fact]
        public void Throws_untransmutable_specification()
        {
            Assert.Throws<UnparsableSpecificationException>(
                context.ActVerifyUntransmutableSpec
            );
        }

        [Fact]
        public void Throws_untransmutable_nested_specification()
        {
            Assert.Throws<UnparsableSpecificationException>(
                context.ActVerifyNestedUntransmutableSpec
            );
        }
    }
}
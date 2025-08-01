using White.Knight.Domain;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;
using White.Knight.Tests.Domain.Specifications;

namespace White.Knight.Tests.Abstractions.Spec
{
    public partial class SpecificationTestContextBase<TResponse>
    {
        protected ICommandTranslator<Customer, TResponse> Sut;

        public virtual void ActVerifyTransmutabilityOfAssembly()
        {
            SpecUtility<Customer, Customer, TResponse>
                .VerifyTransmutabilityOfAllSpecs(Sut, SpecificationAssembly);
        }

        public virtual void ActVerifyUntransmutableSpec()
        {
            SpecUtility<Customer, Customer, TResponse>
                .VerifyTransmutabilityOfSpec(Sut, typeof(SpecificationThatIsNotCompatible<Customer>));
        }

        public virtual void ActVerifyNestedUntransmutableSpec()
        {
            var spec = new SpecificationByAnd<Customer>(
                new CustomerSpecByCustomerName("Test"),
                new SpecificationThatIsNotCompatible<Customer>()
            );

            SpecUtility<Customer, Customer, TResponse>
                .VerifyTransmutabilityOfSpec(Sut, spec);
        }
    }
}
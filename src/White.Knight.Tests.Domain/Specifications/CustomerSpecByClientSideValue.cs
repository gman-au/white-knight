using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByClientSideValue(int year) : SpecificationThatIsNotCompatible<Customer>
    {
        public override bool IsSatisfiedBy(Customer entity)
        {
            return entity.CustomerCreated.Year > year;
        }
    }
}
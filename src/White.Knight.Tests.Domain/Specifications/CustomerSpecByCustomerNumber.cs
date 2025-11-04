using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNumber(int value)
        : SpecificationByEquals<Customer, int>(o => o.CustomerNumber, value);
}
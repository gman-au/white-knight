using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNumber(int value)
        : SpecificationByEquals<Customer>(value, o => o.CustomerNumber);
}
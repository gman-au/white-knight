using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByType(int value)
        : SpecificationByEquals<Customer, int>(o => o.CustomerType, value);
}
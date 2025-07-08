using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByType(int value)
        : SpecificationByEquals<Customer>(value, o => o.CustomerType);
}
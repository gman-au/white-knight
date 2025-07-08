using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerName(string value)
        : SpecificationByEquals<Customer>(value, o => o.CustomerName);
}
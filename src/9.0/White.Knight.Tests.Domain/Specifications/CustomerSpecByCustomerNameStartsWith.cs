using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameStartsWith(string value)
        : SpecificationByTextStartsWith<Customer>(value, o => o.CustomerName);
}
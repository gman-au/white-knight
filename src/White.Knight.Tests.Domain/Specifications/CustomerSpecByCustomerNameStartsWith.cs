using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameStartsWith(string value)
        : SpecificationByTextStartsWith<Customer>(o => o.CustomerName, value);
}
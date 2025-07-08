using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameExact(string value)
        : SpecificationByTextExact<Customer>(value, o => o.CustomerName);
}
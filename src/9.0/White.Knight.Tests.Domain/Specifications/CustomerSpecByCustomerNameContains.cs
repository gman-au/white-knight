using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameContains(string value)
        : SpecificationByTextContains<Customer>(o => o.CustomerName, value);
}
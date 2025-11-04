using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerName(string value)
        : SpecificationByEquals<Customer, string>(o => o.CustomerName, value);
}
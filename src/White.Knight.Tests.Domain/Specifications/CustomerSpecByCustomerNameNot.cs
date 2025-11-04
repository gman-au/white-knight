using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerNameNot(string value)
        : SpecificationByNot<Customer>(new SpecificationByEquals<Customer, string>(o => o.CustomerName, value));
}
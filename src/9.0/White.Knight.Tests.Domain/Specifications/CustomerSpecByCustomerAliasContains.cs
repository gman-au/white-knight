using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerAliasContains(string value)
        : SpecificationByTextContains<Customer>(o => o.CustomerAlias, value);
}
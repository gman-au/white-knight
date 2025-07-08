using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByCustomerAliasContains(string value)
        : SpecificationByTextContains<Customer>(value, o => o.CustomerAlias);
}
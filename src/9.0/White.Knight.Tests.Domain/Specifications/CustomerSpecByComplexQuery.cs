using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByComplexQuery(string value)
        : SpecificationByOr<Customer>(new SpecificationByEquals<Customer, string>(o => o.CustomerName, value),
            new SpecificationByNot<Customer>(new SpecificationByEquals<Customer, int>(o => o.CustomerNumber, 400)));
}
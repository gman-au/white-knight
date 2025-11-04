using System;
using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByOtherGuid(Guid value)
        : SpecificationByEquals<Customer, Guid>(o => o.OtherGuid, value);
}
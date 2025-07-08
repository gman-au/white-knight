using System;
using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByOtherGuid(Guid value)
        : SpecificationByEquals<Customer>(value, o => o.OtherGuid);
}
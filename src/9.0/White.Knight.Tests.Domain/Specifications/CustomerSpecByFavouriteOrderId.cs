using System;
using White.Knight.Abstractions.Specifications;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByFavouriteOrderId(Guid value) : SpecificationByEquals<Customer>(value,
        o => o.FavouriteOrder.OrderId);
}
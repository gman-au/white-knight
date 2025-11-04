using System;
using White.Knight.Domain;

namespace White.Knight.Tests.Domain.Specifications
{
    public class CustomerSpecByFavouriteOrderId(Guid value) : SpecificationByEquals<Customer, Guid>(
        o => o.FavouriteOrder == null ? Guid.Empty : o.FavouriteOrder.OrderId, value);
}
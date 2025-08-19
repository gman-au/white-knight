using System;
using System.Collections.Generic;

namespace White.Knight.Tests.Domain
{
    public sealed class ProjectedCustomer
    {
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<ProjectedOrder> ProjectedOrders { get; set; } = new List<ProjectedOrder>();
    }
}
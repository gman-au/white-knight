using System;

namespace White.Knight.Tests.Domain
{
    public class ProjectedOrder
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string OrderKey { get; set; }

        public DateTime OrderCreated { get; set; }
    }
}
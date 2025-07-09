using System.Collections.Generic;
using White.Knight.Tests.Domain;

namespace White.Knight.Tests.Abstractions.Data
{
    public class AbstractedRepositoryTestData
    {
        public IEnumerable<Address> Addresses { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
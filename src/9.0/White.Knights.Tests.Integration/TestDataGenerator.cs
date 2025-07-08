using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using White.Knight.Tests.Domain;

namespace White.Knights.Tests.Integration
{
    public class TestDataGenerator : ITestDataGenerator
    {
        private readonly Guid[] _customerIds =
        [
            Guid.Parse
                ("0af8f23dbb9046dca90144ca6d801df7"),
            Guid.Parse
                ("7a54ff66f8a54e629091f05ee97695fd"),
            Guid.Parse
                ("cb8cb1df299f4ea7aa2d775f0d3610d0"),
            Guid.Parse
                ("d53ace2b2505471db893b1b1b7af270b")
        ];

        private readonly IFixture _fixture;

        private readonly Guid _otherGuid =
            Guid
                .NewGuid();

        public TestDataGenerator()
        {
            _fixture =
                new Fixture()
                    .Customize(new AutoMoqCustomization());

            _fixture
                .Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture
                .Behaviors
                .Add(new OmitOnRecursionBehavior());
        }

        public AbstractedTestData BuildRepositoryTestData()
        {
            var countries = GenerateCountries();
            var addresses = GenerateAddresses(countries);
            var orders = GenerateOrders();
            var customers = GenerateCustomers(orders);

            return new AbstractedTestData
            {
                Addresses = addresses,
                Customers = customers,
                Orders = orders
            };
        }

        private Country[] GenerateCountries()
        {
            var countries = new List<Country>();

            countries
                .AddRange
                (
                    _fixture
                        .Build<Country>()
                        .CreateMany
                            (4)
                        .ToList()
                );

            return
                countries
                    .ToArray();
        }

        private Address[] GenerateAddresses(Country[] countries)
        {
            var addresses = new List<Address>();

            addresses
                .AddRange
                (
                    _fixture
                        .Build<Address>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[0]
                        )
                        .With
                        (
                            u => u.CountryId,
                            countries[0].CountryId
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (2)
                        .ToList()
                );

            addresses
                .AddRange
                (
                    _fixture
                        .Build<Address>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[1]
                        )
                        .With
                        (
                            u => u.CountryId,
                            countries[3].CountryId
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (2)
                        .ToList()
                );

            return
                addresses
                    .ToArray();
        }

        private Order[] GenerateOrders()
        {
            var orders = new List<Order>();

            orders
                .AddRange
                (
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[0]
                        )
                        .With
                        (
                            u => u.Active,
                            false
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (5)
                        .ToList()
                );

            orders
                .AddRange
                (
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[1]
                        )
                        .With
                        (
                            u => u.Active,
                            false
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (5)
                        .ToList()
                );

            orders
                .AddRange
                (
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[1]
                        )
                        .With
                        (
                            u => u.Active,
                            true
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (5)
                        .ToList()
                );

            orders
                .AddRange
                (
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[2]
                        )
                        .With
                        (
                            u => u.Active,
                            false
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (15)
                        .ToList()
                );

            orders
                .AddRange
                (
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[2]
                        )
                        .With
                        (
                            u => u.Active,
                            true
                        )
                        .Without
                            (u => u.Customer)
                        .CreateMany
                            (5)
                        .ToList()
                );

            orders
                .Add(
                    _fixture
                        .Build<Order>()
                        .With
                        (
                            u => u.CustomerId,
                            _customerIds[2]
                        )
                        .With(
                            u => u.OrderKey,
                            "HARD_TO_FIND"
                        )
                        .With
                        (
                            u => u.Active,
                            true
                        )
                        .Without
                            (u => u.Customer)
                        .Create()
                );

            return
                orders
                    .ToArray();
        }

        private Customer[] GenerateCustomers(Order[] orders)
        {
            var customer =
                _fixture
                    .Build<Customer>()
                    .Without(u => u.FavouriteOrder)
                    .With
                    (
                        u => u.CustomerId,
                        _customerIds[0]
                    )
                    .With
                    (
                        u => u.OtherGuid,
                        _otherGuid
                    )
                    .With
                    (
                        u => u.CustomerType,
                        (int)CustomerTypeEnum.New
                    )
                    .With
                    (
                        u => u.CustomerName,
                        "Arthur"
                    )
                    .With
                    (
                        u => u.CustomerNumber,
                        100
                    )
                    .With
                    (
                        u => u.Orders,
                        orders.Where(x => x.CustomerId == _customerIds[0]).ToArray()
                    )
                    .Without
                        (u => u.Addresses)
                    .Create();

            var customers = new List<Customer>
            {
                customer,
                _fixture
                    .Build<Customer>()
                    .Without(u => u.FavouriteOrder)
                    .With
                    (
                        u => u.CustomerId,
                        _customerIds[1]
                    )
                    .With
                    (
                        u => u.OtherGuid,
                        _otherGuid
                    )
                    .With
                    (
                        u => u.CustomerType,
                        (int)CustomerTypeEnum.New
                    )
                    .With
                    (
                        u => u.CustomerName,
                        "Bradley"
                    )
                    .With
                    (
                        u => u.CustomerNumber,
                        200
                    )
                    .With
                    (
                        u => u.Orders,
                        orders.Where(x => x.CustomerId == _customerIds[1]).ToArray()
                    )
                    .Without
                        (u => u.Addresses)
                    .Create(),

                _fixture
                    .Build<Customer>()
                    .Without(u => u.FavouriteOrder)
                    .With
                    (
                        u => u.CustomerId,
                        _customerIds[2]
                    )
                    .With
                    (
                        u => u.OtherGuid,
                        Guid.NewGuid()
                    )
                    .With
                    (
                        u => u.CustomerType,
                        (int)CustomerTypeEnum.New
                    )
                    .With
                    (
                        u => u.CustomerName,
                        "Casper"
                    )
                    .With
                    (
                        u => u.CustomerNumber,
                        300
                    )
                    .With
                    (
                        u => u.Orders,
                        orders.Where(x => x.CustomerId == _customerIds[2]).ToArray()
                    )
                    .Without
                        (u => u.Addresses)
                    .Create(),

                _fixture
                    .Build<Customer>()
                    .With(
                        u => u.FavouriteOrder,
                        new Order
                        {
                            OrderId = Guid.Parse("543a0e8f-fac0-4a64-8ef4-dccaa33b29b0"),
                            CustomerId = _customerIds[2],
                            OrderKey = "MY_ORDER_KEY"
                        }
                    )
                    .With
                    (
                        u => u.CustomerId,
                        _customerIds[3]
                    )
                    .With
                    (
                        u => u.OtherGuid,
                        Guid.NewGuid()
                    )
                    .With
                    (
                        u => u.CustomerType,
                        (int)CustomerTypeEnum.Returning
                    )
                    .With
                    (
                        u => u.CustomerName,
                        "Arthur"
                    )
                    .With
                    (
                        u => u.CustomerNumber,
                        400
                    )
                    .With
                    (
                        u => u.Orders,
                        orders.Where(x => x.CustomerId == _customerIds[3]).ToArray()
                    )
                    .Without
                        (u => u.Addresses)
                    .Create()
            };

            return
                customers
                    .ToArray();
        }
    }
}
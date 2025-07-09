using System;
using System.Linq;
using System.Threading.Tasks;
using White.Knight.Abstractions.Extensions;
using White.Knight.Abstractions.Fluent;
using White.Knight.Abstractions.Specifications;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;
using White.Knight.Tests.Domain.Specifications;
using White.Knights.Tests.Integration.Extensions;

namespace White.Knights.Tests.Integration.Context
{
    public partial class TestContextBase
    {
        private IRepository<Customer> _sut;

        public async Task ActSearchByAllAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new SpecificationByAll<Customer>()
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByValidKeyAsync()
        {
            var result =
                await
                    _sut
                        .SingleRecordAsync
                        (
                            Guid
                                .Parse
                                    ("0af8f23dbb9046dca90144ca6d801df7")
                                .ToSingleRecordCommand<Customer>()
                        );

            _results =
                RecordEx
                    .ToMockResults(result);
        }

        public async Task ActSearchByInvalidKeyAsync()
        {
            var result =
                await
                    _sut
                        .SingleRecordAsync
                            (49.0d);

            _results =
                RecordEx
                    .ToMockResults(result);
        }

        public async Task ActSearchWithPageSizeTwoAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new SpecificationByAll<Customer>()
                                .ToQueryCommand()
                                .WithPaging
                                (
                                    1,
                                    2
                                )
                        );
        }

        public async Task ActSearchByCustomerNumberAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNumber
                                    (400)
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByOrCustomerNumberAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNumber
                                    (100)
                                .Or
                                (
                                    new CustomerSpecByCustomerNumber
                                            (200)
                                        .Or
                                        (
                                            new CustomerSpecByCustomerNumber
                                                (250)
                                        )
                                )
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByCustomerTypeAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByType((int)CustomerTypeEnum.New)
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByOtherGuidAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByOtherGuid(Guid.Parse("f12ace2b2505671db811b10fb73ed714"))
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByNameAndNumberAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerName
                                    ("Arthur")
                                .And
                                (
                                    new CustomerSpecByCustomerNumber
                                        (400)
                                )
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByFavouriteOrderIdAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByFavouriteOrderId
                                    (Guid.Parse("543a0e8f-fac0-4a64-8ef4-dccaa33b29b0"))
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByExampleAutoCompleteTextAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            (new SpecificationByAll<Customer>() &
                             (new SpecificationByNone<Customer>() |
                              new CustomerSpecByCustomerNameContains("thur") |
                              new CustomerSpecByCustomerAliasContains("thur")
                             ))
                            .ToQueryCommand()
                        );
        }

        public async Task ActSearchByNameExactAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameExact
                                    ("Arthur")
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByNameContainsAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameContains
                                    ("rt")
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchByNameStartsWithAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameStartsWith
                                    ("Art")
                                .ToQueryCommand()
                        );
        }

        public async Task ActSearchSortByNumberDescAsync()
        {
            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new SpecificationByAll<Customer>()
                                .ToQueryCommand()
                                .WithSorting(
                                    o => o.CustomerNumber,
                                    true
                                )
                        );
        }

        public async Task ActUpdateAsSuppliedAsync()
        {
            var customer =
                _abstractedTestData
                    .Customers
                    .ElementAt(0);

            customer.CustomerName = "Jeff";
            customer.CustomerNumber = 200;

            _result =
                await
                    _sut
                        .AddOrUpdateAsync
                        (
                            customer
                                .ToUpdateCommand()
                        );
        }

        public async Task ActUpdateWithExcludingAsync()
        {
            var customer =
                _abstractedTestData
                    .Customers
                    .ElementAt(0);

            customer.CustomerName = "Jeff";
            customer.CustomerNumber = 200;

            _result =
                await
                    _sut
                        .AddOrUpdateAsync
                        (
                            customer
                                .ToUpdateCommand()
                                .WithoutUpdating
                                    (o => o.CustomerName)
                        );
        }

        public async Task ActUpdateWithIncludingAsync()
        {
            var customer =
                _abstractedTestData
                    .Customers
                    .ElementAt(0);

            customer.CustomerName = "Jeff";
            customer.CustomerNumber = 200;

            _result =
                await
                    _sut
                        .AddOrUpdateAsync
                        (
                            customer
                                .ToUpdateCommand()
                                .OnlyUpdating
                                    (o => o.CustomerName)
                        );
        }

        public async Task ActAddAsync()
        {
            var newCustomer =
                _abstractedTestData
                    .Customers
                    .ElementAt(0);

            newCustomer.CustomerId =
                Guid
                    .Empty;

            _result =
                await
                    _sut
                        .AddOrUpdateAsync
                        (
                            newCustomer
                                .ToUpdateCommand()
                        );
        }

        public async Task ActDeleteCustomerAsync()
        {
            _result =
                _abstractedTestData
                    .Customers
                    .ElementAt(2);

            var customerIdToDelete =
                _result
                    .CustomerId;

            await
                _sut
                    .DeleteRecordAsync
                    (
                        customerIdToDelete
                            .ToSingleRecordCommand<Customer>()
                    );

            _results =
                await
                    _sut
                        .QueryAsync
                        (
                            new SpecificationByAll<Customer>()
                                .ToQueryCommand()
                        );
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using White.Knight.Abstractions.Extensions;
using White.Knight.Abstractions.Fluent;
using White.Knight.Abstractions.Specifications;
using White.Knight.Interfaces;
using White.Knight.Tests.Domain;
using White.Knight.Tests.Domain.Specifications;
using White.Knights.Tests.Abstractions.Extensions;

namespace White.Knights.Tests.Abstractions.Repository
{
    public partial class RepositoryTestContextBase
    {
        private IRepository<Customer> _sut;

        public virtual async Task ActSearchByAllAsync()
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

        public virtual async Task ActSearchByValidKeyAsync()
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

        public virtual async Task ActSearchByInvalidKeyAsync()
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

        public virtual async Task ActSearchWithPageSizeTwoAsync()
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

        public virtual async Task ActSearchByCustomerNumberAsync()
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

        public virtual async Task ActSearchByOrCustomerNumberAsync()
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

        public virtual async Task ActSearchByCustomerTypeAsync()
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

        public virtual async Task ActSearchByOtherGuidAsync()
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

        public virtual async Task ActSearchByNameAndNumberAsync()
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

        public virtual async Task ActSearchByFavouriteOrderIdAsync()
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

        public virtual async Task ActSearchByExampleAutoCompleteTextAsync()
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

        public virtual async Task ActSearchByNameExactAsync()
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

        public virtual async Task ActSearchByNameContainsAsync()
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

        public virtual async Task ActSearchByNameStartsWithAsync()
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

        public virtual async Task ActSearchSortByNumberDescAsync()
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

        public virtual async Task ActUpdateAsSuppliedAsync()
        {
            var customer =
                _abstractedRepositoryTestData
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

        public virtual async Task ActUpdateWithExcludingAsync()
        {
            var customer =
                _abstractedRepositoryTestData
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

        public virtual async Task ActUpdateWithIncludingAsync()
        {
            var customer =
                _abstractedRepositoryTestData
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

        public virtual async Task ActAddAsync()
        {
            var newCustomer =
                _abstractedRepositoryTestData
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

        public virtual async Task ActDeleteCustomerAsync()
        {
            _result =
                _abstractedRepositoryTestData
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

        public virtual async Task ActSearchWithNonNestedProjection()
        {
            var command =
                new CustomerSpecByType
                        ((int)CustomerTypeEnum.New)
                    .ToQueryCommand()
                    .WithProjection
                    (
                        o => new ProjectedCustomer
                        {
                            CustomerId = o.CustomerId,
                            CustomerName = o.CustomerName
                        }
                    );

            _projectedResults =
                await
                    _sut
                        .QueryAsync
                            (command);
        }
    }
}
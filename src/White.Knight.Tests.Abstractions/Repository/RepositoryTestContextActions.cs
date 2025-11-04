using System;
using System.Linq;
using System.Threading.Tasks;
using White.Knight.Abstractions.Fluent;
using White.Knight.Domain;
using White.Knight.Interfaces;
using White.Knight.Tests.Abstractions.Extensions;
using White.Knight.Tests.Domain;
using White.Knight.Tests.Domain.Specifications;

namespace White.Knight.Tests.Abstractions.Repository
{
    public partial class RepositoryTestContextBase
    {
        protected IRepository<Customer> Sut;

        public virtual async Task ActSearchByAllAsync()
        {
            Results =
                await
                    Sut
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
                    Sut
                        .SingleRecordAsync
                        (
                            Guid
                                .Parse
                                    ("0af8f23dbb9046dca90144ca6d801df7")
                                .ToSingleRecordCommand<Customer>()
                        );

            Results =
                RecordEx
                    .ToMockResults(result);
        }

        public virtual async Task ActSearchByInvalidKeyAsync()
        {
            var result =
                await
                    Sut
                        .SingleRecordAsync
                            (49.0d);

            Results =
                RecordEx
                    .ToMockResults(result);
        }

        public virtual async Task ActSearchWithPageSizeTwoAsync()
        {
            Results =
                await
                    Sut
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
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNumber
                                    (400)
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByOrCustomerNumberAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            (
                                new CustomerSpecByCustomerNumber
                                    (100) |
                                new CustomerSpecByCustomerNumber
                                    (200) |
                                new CustomerSpecByCustomerNumber
                                    (250)
                            )
                            .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByCustomerTypeAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByType((int)CustomerTypeEnum.New)
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByOtherGuidAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByOtherGuid(Guid.Parse("f12ace2b2505671db811b10fb73ed714"))
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByNameAndNumberAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            (
                                new CustomerSpecByCustomerName
                                    ("Arthur") &
                                new CustomerSpecByCustomerNumber
                                    (400)
                            )
                            .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByFavouriteOrderIdAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByFavouriteOrderId
                                    (Guid.Parse("543a0e8f-fac0-4a64-8ef4-dccaa33b29b0"))
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByClientSideForcedEvaluation()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByClientSideValue(2005)
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByExampleAutoCompleteTextAsync()
        {
            Results =
                await
                    Sut
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
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameExact
                                    ("Arthur")
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByNameExactNotAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameNot
                                    ("Arthur")
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByNameContainsAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameContains
                                    ("rt")
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchByNameStartsWithAsync()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new CustomerSpecByCustomerNameStartsWith
                                    ("Art")
                                .ToQueryCommand()
                        );
        }

        public virtual async Task ActSearchSortByNumberDescAsync()
        {
            Results =
                await
                    Sut
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

            Result =
                await
                    Sut
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

            Result =
                await
                    Sut
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

            Result =
                await
                    Sut
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

            Result =
                await
                    Sut
                        .AddOrUpdateAsync
                        (
                            newCustomer
                                .ToUpdateCommand()
                        );
        }

        public virtual async Task ActDeleteCustomerAsync()
        {
            Result =
                _abstractedRepositoryTestData
                    .Customers
                    .ElementAt(2);

            var customerIdToDelete =
                Result
                    .CustomerId;

            await
                Sut
                    .DeleteRecordAsync
                    (
                        customerIdToDelete
                            .ToSingleRecordCommand<Customer>()
                    );

            Results =
                await
                    Sut
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
                    (o => new ProjectedCustomer
                        {
                            CustomerId = o.CustomerId,
                            CustomerName = o.CustomerName
                        }
                    );

            ProjectedResults =
                await
                    Sut
                        .QueryAsync
                            (command);
        }

        public virtual async Task ActSearchWithUnparsableSpec()
        {
            Results =
                await
                    Sut
                        .QueryAsync
                        (
                            new SpecificationThatIsNotCompatible<Customer>()
                                .ToQueryCommand()
                        );
        }
    }
}
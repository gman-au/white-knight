using System;
using System.Linq;
using System.Threading.Tasks;
using White.Knight.Definition;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knight.Tests.Abstractions.Repository
{
    public partial class RepositoryTestContextBase
    {
        private Customer _result;
        private RepositoryResult<Customer> _results;
        private RepositoryResult<ProjectedCustomer> _projectedResults;

        public void AssertKeyRecordIsReturned()
        {
            Assert.NotEmpty(_results?.Records ?? []);

            Assert.Equal
            (
                1,
                _results?.Count
            );
            Assert.Equal
            (
                Guid.Parse
                    ("0af8f23dbb9046dca90144ca6d801df7"),
                _results?.Records?.ElementAt
                        (0)
                    .CustomerId
            );
        }

        public void AssertInvalidKeyExceptionWasThrown(Task task)
        {
            Assert.True
                (task.IsFaulted);
            Assert.NotNull
                (task.Exception);
            var exceptions = task.Exception.InnerExceptions;
            Assert.NotEmpty
                (exceptions);
            Assert.Equal
            (
                "Could not use key System.Double against repository of type White.Knight.Tests.Domain.Customer",
                exceptions.ElementAt
                        (0)
                    .Message
            );
        }

        public void AssertRecordCount(int expectedCount)
        {
            Assert.NotEmpty
                (_results?.Records ?? []);
            Assert.Equal
            (
                expectedCount,
                _results?.Count
            );
        }

        public void AssertOneSpecificRecordExists()
        {
            Assert.NotEmpty
                (_results?.Records ?? []);
            Assert.Equal
            (
                1,
                _results?.Count
            );
        }

        public void AssertFirstRecordIs400()
        {
            Assert.NotEmpty
                (_results?.Records ?? []);

            Assert.Equal
            (
                4,
                _results.Records.Count()
            );

            var firstRecord =
                _results
                    .Records
                    .ElementAt(0);

            Assert.Equal(
                400,
                firstRecord.CustomerNumber
            );
        }

        public void AssertNoPropertiesPreserved()
        {
            Assert.NotNull
                (_result);
            Assert.Equal
            (
                200,
                _result.CustomerNumber
            );
            Assert.Equal
            (
                "Jeff",
                _result.CustomerName
            );
        }

        public void AssertUpdatesWereExcluded()
        {
            Assert.NotNull
                (_result);
            Assert.Equal
            (
                200,
                _result.CustomerNumber
            );
            Assert.Equal
            (
                "Arthur",
                _result.CustomerName
            );
        }

        public void AssertUpdatesWereIncluded()
        {
            Assert.NotNull
                (_result);
            Assert.Equal
            (
                100,
                _result.CustomerNumber
            );
            Assert.Equal
            (
                "Jeff",
                _result.CustomerName
            );
        }

        public void AssertResultIsNotNull()
        {
            Assert
                .NotNull
                    (_result);
        }

        public void AssertDeletedRecordNotPresent()
        {
            Assert.NotEmpty
                (_results?.Records ?? []);
            Assert.Equal
            (
                3,
                _results?.Count
            );
            Assert.DoesNotContain
            (
                _results.Records,
                r => r.CustomerId == _result.CustomerId
            );
        }

        public void AssertRecordsAreProjectedWithoutNesting()
        {
            Assert.NotEmpty(_projectedResults?.Records ?? []);
            Assert.Equal
            (
                3,
                _projectedResults?.Count
            );

            var customer =
                _projectedResults
                    .Records
                    .ElementAt(0);

            Assert.Null(customer.Orders);
        }
    }
}
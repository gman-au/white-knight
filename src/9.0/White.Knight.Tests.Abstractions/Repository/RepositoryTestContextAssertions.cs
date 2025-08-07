using System;
using System.Linq;
using System.Threading.Tasks;
using White.Knight.Domain;
using White.Knight.Tests.Domain;
using Xunit;

namespace White.Knight.Tests.Abstractions.Repository
{
    public partial class RepositoryTestContextBase
    {
        protected RepositoryResult<ProjectedCustomer> ProjectedResults;
        protected Customer Result;
        protected RepositoryResult<Customer> Results;

        public void AssertKeyRecordIsReturned()
        {
            Assert.NotEmpty(Results?.Records ?? []);

            Assert.Equal
            (
                1,
                Results?.Count
            );
            Assert.Equal
            (
                Guid.Parse
                    ("0af8f23dbb9046dca90144ca6d801df7"),
                Results?.Records?.ElementAt
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
                (Results?.Records ?? []);
            Assert.Equal
            (
                expectedCount,
                Results?.Count
            );
        }

        public void AssertOneSpecificRecordExists()
        {
            Assert.NotEmpty
                (Results?.Records ?? []);
            Assert.Equal
            (
                1,
                Results?.Count
            );
        }

        public void AssertFirstRecordIs400()
        {
            Assert.NotEmpty
                (Results?.Records ?? []);

            Assert.Equal
            (
                4,
                Results.Records.Count()
            );

            var firstRecord =
                Results
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
                (Result);
            Assert.Equal
            (
                200,
                Result.CustomerNumber
            );
            Assert.Equal
            (
                "Jeff",
                Result.CustomerName
            );
        }

        public void AssertUpdatesWereExcluded()
        {
            Assert.NotNull
                (Result);
            Assert.Equal
            (
                200,
                Result.CustomerNumber
            );
            Assert.Equal
            (
                "Arthur",
                Result.CustomerName
            );
        }

        public void AssertUpdatesWereIncluded()
        {
            Assert.NotNull
                (Result);
            Assert.Equal
            (
                100,
                Result.CustomerNumber
            );
            Assert.Equal
            (
                "Jeff",
                Result.CustomerName
            );
        }

        public void AssertResultIsNotNull()
        {
            Assert
                .NotNull
                    (Result);
        }

        public void AssertDeletedRecordNotPresent()
        {
            Assert.NotEmpty
                (Results?.Records ?? []);
            Assert.Equal
            (
                3,
                Results?.Count
            );
            Assert.DoesNotContain
            (
                Results.Records,
                r => r.CustomerId == Result.CustomerId
            );
        }

        public void AssertRecordsAreProjectedWithoutNesting()
        {
            Assert.NotEmpty(ProjectedResults?.Records ?? []);
            Assert.Equal
            (
                3,
                ProjectedResults?.Count
            );

            var customer =
                ProjectedResults
                    .Records
                    .ElementAt(0);

            Assert.Null(customer.Orders);
        }

        public void AssertExactlyNotRecordCount()
        {
            Assert.NotEmpty
                (Results?.Records ?? []);
            Assert.Equal
            (
                2,
                Results?.Count
            );
            Assert.DoesNotContain
            (
                Results.Records,
                r => r.CustomerName == "Arthur"
            );
        }
    }
}
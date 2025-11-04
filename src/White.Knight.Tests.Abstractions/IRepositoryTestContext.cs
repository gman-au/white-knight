using System.Threading.Tasks;

namespace White.Knight.Tests.Abstractions
{
    public interface IRepositoryTestContext
    {
        Task ArrangeRepositoryDataAsync();

        Task ActSearchByAllAsync();

        Task ActSearchByValidKeyAsync();

        Task ActSearchByInvalidKeyAsync();

        Task ActSearchWithPageSizeTwoAsync();

        Task ActSearchByCustomerNumberAsync();

        Task ActSearchByOrCustomerNumberAsync();

        Task ActSearchByCustomerTypeAsync();

        Task ActSearchByOtherGuidAsync();

        Task ActSearchByNameAndNumberAsync();

        Task ActSearchByFavouriteOrderIdAsync();

        Task ActSearchByExampleAutoCompleteTextAsync();

        Task ActSearchByNameExactAsync();

        Task ActSearchByNameExactNotAsync();

        Task ActSearchByNameContainsAsync();

        Task ActSearchByNameStartsWithAsync();

        Task ActSearchSortByNumberDescAsync();

        Task ActUpdateAsSuppliedAsync();

        Task ActUpdateWithExcludingAsync();

        Task ActUpdateWithIncludingAsync();

        Task ActAddAsync();

        Task ActDeleteCustomerAsync();

        Task ActSearchWithNonNestedProjection();

        Task ActSearchByClientSideForcedEvaluation();

        void AssertKeyRecordIsReturned();

        void AssertInvalidKeyExceptionWasThrown(Task task);

        void AssertRecordCount(int expectedCount);

        void AssertOneSpecificRecordExists();

        void AssertNoPropertiesPreserved();

        void AssertUpdatesWereExcluded();

        void AssertUpdatesWereIncluded();

        void AssertResultIsNotNull();

        void AssertFirstRecordIs400();

        void AssertDeletedRecordNotPresent();

        void AssertRecordsAreProjectedWithoutNesting();

        void AssertExactlyNotRecordCount();
    }
}
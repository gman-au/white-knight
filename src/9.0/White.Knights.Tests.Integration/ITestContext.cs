using System.Threading.Tasks;

namespace White.Knights.Tests.Integration
{
    public interface ITestContext
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

        Task ActSearchByNameContainsAsync();

        Task ActSearchByNameStartsWithAsync();

        Task ActSearchSortByNumberDescAsync();

        void AssertKeyRecordIsReturned();

        void AssertInvalidKeyExceptionWasThrown(Task task);

        void AssertRecordCount(int expectedCount);

        void AssertOneSpecificRecordExists(int expectedNumber = 400);
    }
}
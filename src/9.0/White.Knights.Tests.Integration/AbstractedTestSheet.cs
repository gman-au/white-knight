using System.Threading.Tasks;
using Xunit;

namespace White.Knights.Tests.Integration
{
    public abstract class AbstractedTestSheet(ITestContext context)
    {
        [Fact]
        public async Task Test_Retrieve_Single_Record()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByValidKeyAsync();

            context
                .AssertKeyRecordIsReturned();
        }

        [Fact]
        public async Task Test_Invalid_Key_Search()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            var task =
                context
                    .ActSearchByInvalidKeyAsync();

            context
                .AssertInvalidKeyExceptionWasThrown(task);
        }

        [Fact]
        public async Task Test_Search_All_Users()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByAllAsync();

            context
                .AssertRecordCount(4);
        }

        [Fact]
        public async Task Test_Search_Split_Page()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchWithPageSizeTwoAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Search_By_Number()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByCustomerNumberAsync();

            context
                .AssertOneSpecificRecordExists();
        }

        [Fact]
        public async Task Test_Search_With_Or()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByOrCustomerNumberAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Search_By_Enum()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByCustomerTypeAsync();

            context
                .AssertRecordCount(3);
        }

        [Fact]
        public async Task Test_Search_By_Guid()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByOtherGuidAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Search_With_And()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByNameAndNumberAsync();

            context
                .AssertOneSpecificRecordExists();
        }

        [Fact]
        public async Task Test_Search_By_Sub_Item_Query_Id()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByFavouriteOrderIdAsync();

            context
                .AssertOneSpecificRecordExists();
        }

        [Fact]
        public async Task Test_Search_By_Example_AutoComplete_Text()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByExampleAutoCompleteTextAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Name_Exact()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByNameExactAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Name_Contains()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByNameContainsAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Name_Starts_With()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchByNameStartsWithAsync();

            context
                .AssertRecordCount(2);
        }

        [Fact]
        public async Task Test_Sort_By_Number()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchSortByNumberDescAsync();

            context
                .AssertOneSpecificRecordExists();
        }
    }
}
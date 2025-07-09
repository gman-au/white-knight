using System.Threading.Tasks;
using Xunit;

namespace White.Knight.Tests.Abstractions.Tests
{
    public abstract class AbstractedRepositoryTests(IRepositoryTestContext context)
    {
        [Fact]
        public virtual async Task Test_Retrieve_Single_Record()
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
        public virtual async Task Test_Invalid_Key_Search()
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
        public virtual async Task Test_Search_All_Users()
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
        public virtual async Task Test_Search_Split_Page()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchWithPageSizeTwoAsync();

            context
                .AssertRecordCount(4);
        }

        [Fact]
        public virtual async Task Test_Search_By_Number()
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
        public virtual async Task Test_Search_With_Or()
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
        public virtual async Task Test_Search_By_Enum()
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
        public virtual async Task Test_Search_By_Guid()
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
        public virtual async Task Test_Search_With_And()
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
        public virtual async Task Test_Search_By_Sub_Item_Query_Id()
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
        public virtual async Task Test_Search_By_Example_AutoComplete_Text()
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
        public virtual async Task Test_Name_Exact()
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
        public virtual async Task Test_Name_Contains()
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
        public virtual async Task Test_Name_Starts_With()
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
        public virtual async Task Test_Sort_By_Number()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchSortByNumberDescAsync();

            context
                .AssertFirstRecordIs400();
        }

        [Fact]
        public virtual async Task Test_Update_As_Supplied()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActUpdateAsSuppliedAsync();

            context
                .AssertNoPropertiesPreserved();
        }

        [Fact]
        public virtual async Task Test_Update_Excluding_Fields()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActUpdateWithExcludingAsync();

            context
                .AssertUpdatesWereExcluded();
        }

        [Fact]
        public virtual async Task Test_Update_Including_Fields()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActUpdateWithIncludingAsync();

            context
                .AssertUpdatesWereIncluded();
        }

        [Fact]
        public virtual async Task Test_Add()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActAddAsync();

            context
                .AssertResultIsNotNull();
        }

        [Fact]
        public async Task Test_Delete()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActDeleteCustomerAsync();

            context
                .AssertDeletedRecordNotPresent();
        }

        [Fact]
        public async Task Test_Search_With_Non_Nested_Projection()
        {
            await
                context
                    .ArrangeRepositoryDataAsync();

            await
                context
                    .ActSearchWithNonNestedProjection();

            context
                .AssertRecordsAreProjectedWithoutNesting();
        }
    }
}
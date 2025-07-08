using System.Threading.Tasks;
using Xunit;

namespace White.Knights.Tests.Integration
{
    public abstract class AbstractedTestSheet(ITestContext context)
    {
        [Fact]
        public async Task Test_Search_All_Users()
        {
            await
                context
                    .ArrangeTableDataAsync();

            await
                context
                    .ActSearchByAll();

            context
                .AssertRecordCountFour();
        }
    }
}
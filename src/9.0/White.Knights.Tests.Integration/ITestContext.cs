using System.Threading.Tasks;

namespace White.Knights.Tests.Integration
{
    public interface ITestContext
    {
        Task ArrangeTableDataAsync();

        Task ActSearchByAll();

        void AssertRecordCountFour();
    }
}